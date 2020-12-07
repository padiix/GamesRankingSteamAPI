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

        public async void GetDataAndSendToDatabase()
        {
            DateTime currentDate = DateTime.UtcNow.Date;
            var firstDayMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayMonth = firstDayMonth.AddMonths(1).AddDays(-1);
            long unixFirst = ((DateTimeOffset)firstDayMonth).ToUnixTimeSeconds();
            long unixLast = ((DateTimeOffset)lastDayMonth).ToUnixTimeSeconds();
            //long unixLastFixed = unixFirst + 5356798;

            IGDBClient igdb = new IGDBClient(// Found in Twitch Developer portal for your app
                    Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
                    Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET"));

            var top15InterestingGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                    query: "fields id, name, summary, genres.name, age_ratings.rating, first_release_date, total_rating, follows, url; "
                            + "sort follows desc; "
                            + "where follows != null & first_release_date >= "+ unixFirst +"; "
                            + "limit 15; ");


            //2505602 - wartość równego miesiąca w Unix Time Stamp
            //5356798 - wartość miesiąca od 1. (00:00:01) do 31. (23:59:59) w Unix Time Stamp

            Top10populargames TopPopular = new Top10populargames();
            DateTime LastUpdate = (DateTime)TopPopular.Updated;
            TimeSpan timeSpan = currentDate.Subtract(LastUpdate);

            if (TopPopular.Updated == null) // If date in table is null we provide the thing with data and set the update row. 
            {
                var top10PopularGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                    query: "fields id, age_ratings.category, age_ratings.rating, category, first_release_date, rating, rating_count, genres.name, name, summary, url; "
                            + "sort rating_count desc; "
                            + "where(rating > 60) & (rating_count > 8) & (hypes != null) & (category = 0) & (first_release_date >= " + unixFirst + ") & (first_release_date < " + unixLast + "); "
                            + "limit 10;");
                using var context = new gamesrankdbContext();
                {
                    foreach (Game game in top10PopularGames)    //Checking every thing in the top10 popular games object from IGDB API Package.
                    {
                        var ReleaseDate = game.FirstReleaseDate.Value;
                        var TPRecord = new Top10populargames()
                        {
                            GameId = game.Id,
                            Title = game.Name,
                            Summary = game.Summary,
                            FirstReleaseDate = ReleaseDate.UtcDateTime,
                            Rating = game.Rating,
                            RatingCount = game.RatingCount,
                            Updated = currentDate
                        };

                        context.Top10populargames.Add(TPRecord);
                    }
                    context.SaveChanges();
                }
            }
            else if (timeSpan.TotalDays > 6)
            {
                using var context = new gamesrankdbContext();
                {
                    try
                    {
                        //context.Database.ExecuteSqlCommand("TRUNCATE[TABLE] Top10PopularGames;"); // If thing below won't work, i'm gonna try this one.
                        context.Top10populargames.FromSqlRaw("TRUNCATE[TABLE] Top10PopularGames;"); // Not sure if this will really wipe the data...
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        throw new Exception("Failed truncating the tables.");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
