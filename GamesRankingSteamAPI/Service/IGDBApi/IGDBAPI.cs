using IGDB;
using IGDB.Models;
using GamesRankingSteamAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GamesRankingSteamAPI.Service
{
    public class IGDBAPI
    {

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0,
                                                      DateTimeKind.Utc);
        public static DateTime UnixTimeToDateTime(string text)
        {
            double seconds = double.Parse(text, CultureInfo.InvariantCulture);
            return Epoch.AddSeconds(seconds);
        }
        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        public async void GetDataAndSendToDatabase()
        {
            DateTime currentDate = DateTime.UtcNow.Date;
            DateTime threeMonthsBehindUNIX = DateTime.UtcNow.Date.AddMonths(-3);
            var firstDayMonth = new DateTime(threeMonthsBehindUNIX.Year, threeMonthsBehindUNIX.Month, 1);
            var unixFirst = ConvertToUnixTime(firstDayMonth);
            var unixCurr = ConvertToUnixTime(currentDate);

            var igdb = new IGDBClient(Environment.GetEnvironmentVariable("CLIENT_ID"), Environment.GetEnvironmentVariable("SECRET"));

            using var dbcontext = new gamesrankdbContext();
            {
                //
                //      Adding data to Top10PopularGames
                //
                
                #nullable enable
                dynamic FirstRowData = dbcontext.Top10populargames.FirstOrDefault();
                #nullable disable
                if (FirstRowData?.Updated == null) // If date in table is null we provide the thing with data and set the update row. 
                {
                    var top10PopularGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id, rating, rating_count, name;" +
                        " sort rating_count desc;" +
                        " where(rating > 60) & (rating_count > 8) & (hypes != null) & (category = 0) & (first_release_date >= "+unixFirst+") & (first_release_date < "+unixCurr+");" +
                        " limit 10;");

                    foreach (Game game in top10PopularGames)    //Checking every thing in the top10 popular games object from IGDB API Package.
                        {
                            var TPRecord = new Top10populargames()
                            {
                                GameId = game.Id,
                                Title = game.Name,
                                Rating = game.Rating,
                                RatingCount = game.RatingCount,
                                Updated = currentDate
                            };

                            dbcontext.Top10populargames.Add(TPRecord);
                        }
                    await dbcontext.SaveChangesAsync();
                }
                else
                {
                    DateTime LastUpdate = (DateTime)FirstRowData.Updated;
                    TimeSpan timeSpan = currentDate.Subtract(LastUpdate);

                    if (timeSpan.TotalDays > 6)
                    {
                        try
                        {
                            dbcontext.Database.ExecuteSqlRaw("TRUNCATE TABLE top10populargames;"); // Working deletion of all data inside table.
                            await dbcontext.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            throw new Exception("Failed truncating the tables.");
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        var top10PopularGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                        query: "fields id, rating, rating_count, name;" +
                        " sort rating_count desc;" +
                        " where(rating > 60) & (rating_count > 8) & (hypes != null) & (category = 0) & (first_release_date >= " + unixFirst + ") & (first_release_date < " + unixCurr + ");" +
                        " limit 10; ");

                        foreach (Game game in top10PopularGames)    //Checking every thing in the top10 popular games object from IGDB API Package.
                        {
                            var TPRecord = new Top10populargames()
                            {
                                GameId = game.Id,
                                Title = game.Name,
                                Rating = game.Rating,
                                RatingCount = game.RatingCount,
                                Updated = currentDate
                            };

                            dbcontext.Top10populargames.Add(TPRecord);
                        }
                        await dbcontext.SaveChangesAsync();
                    }
                }

                //
                //      Adding data to Top15InterestingGames that has one-to-many relationship with Genres
                //

                //FirstRowData = context.Top15interestinggames.FirstOrDefault();
                //if (FirstRowData?.Updated == null)
                //{
                //    var top15InterestingGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                //    query: "fields id, name, genres.name, first_release_date, url;" +
                //    " sort follows desc;" +
                //    " where follows != null & first_release_date >= "+ unixFirst +";" +
                //    " limit 15; ");

                //    foreach (Game game in top15InterestingGames)
                //    {
                //        List<Genres> GRecord;
                //        var tmp = game.Genres.Values.ToList();

                //        GRecord = tmp.ConvertAll(x => new Genres() 
                //        { 
                //            GenreId = x.Id, 
                //            Name = x.Name,
                //            GamesGameId = game.Id
                //        });

                //        var ReleaseDate = game.FirstReleaseDate.Value;
                //        var TIRecord = new Top15interestinggames()
                //        {
                //            GameId = game.Id,
                //            Title = game.Name,
                //            FirstReleaseDate = ReleaseDate.UtcDateTime,
                //            Url = game.Url,
                //            Updated = currentDate,
                //            Genres = GRecord     
                //        };

                //        //context.Top15interestinggames.Add(TIRecord);
                //    }
                //    //await context.SaveChangesAsync();
                //}
            }
        }
    }
}
