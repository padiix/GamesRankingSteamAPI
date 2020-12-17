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

            using var dbcontextpopular = new gamesrankdbContext();
            {
                //First - check if database is created, if not, stop all actions.
                dbcontextpopular.Database.EnsureCreated();

                //
                //      Adding data to Top10PopularGames
                //

                // /*>> FDR variables are holding the F.irst R.ow D.ata of selected table for checkups. <<*/ //
                Top10populargames? FRDPopular = dbcontextpopular.Top10populargames.FirstOrDefault();

                if (FRDPopular?.Updated == null) // If date in table is null we provide the thing with data and set the update row. 
                {
                    var top10PopularGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id, rating, rating_count, name;" +
                        " sort rating_count desc;" +
                        " where(rating > 60) & (rating_count > 8) & (hypes != null) & (category = 0) & (first_release_date >= " + unixFirst + ") & (first_release_date < " + unixCurr + ");" +
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

                        dbcontextpopular.Top10populargames.Add(TPRecord);
                    }
                    await dbcontextpopular.SaveChangesAsync();
                }
                else
                {
                    DateTime LastUpdate = (DateTime)FRDPopular.Updated;
                    TimeSpan timeSpan = currentDate.Subtract(LastUpdate);

                    if (timeSpan.TotalDays > 6)
                    {
                        try
                        {
                            dbcontextpopular.Database.ExecuteSqlRaw("TRUNCATE TABLE top10populargames;"); // Working deletion of all data inside table.
                            await dbcontextpopular.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            throw new Exception("Failed truncating the tables.");
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        var top10PopularGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id, rating, rating_count, name;" +
                        " sort rating_count desc;" +
                        " where(rating > 60) & (rating_count > 8) & (hypes != null) & (category = 0) & (first_release_date >= " + unixFirst + ") & (first_release_date < " + unixCurr + ");" +
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

                            dbcontextpopular.Top10populargames.Add(TPRecord);
                        }
                        await dbcontextpopular.SaveChangesAsync();
                    }
                }
            }

            using var dbcontextgenre = new gamesrankdbContext();
            {
                //First - check if database is created, if not, stop all actions.
                dbcontextgenre.Database.EnsureCreated();

                //
                //      Adding all genres to the Genres table
                //

                //Second - Check if there's data, if not, then fill table up.
                Genres? FRDGenres = dbcontextgenre.Genres.FirstOrDefault();
                if (FRDGenres?.GenreId == null)
                {
                    var genres = await igdb.QueryAsync<Genre>(IGDBClient.Endpoints.Genres, query: "fields id, name; sort name; limit 50; ");

                    foreach (Genre genre in genres)
                    {
                        var GRecord = new Genres()
                        {
                            GenreId = genre.Id,
                            Name = genre.Name
                        };

                        await dbcontextgenre.Genres.AddAsync(GRecord);
                    }
                    await dbcontextgenre.SaveChangesAsync();
                }
            }

            using var dbcontextinteresting = new gamesrankdbContext();
            {
                //First - check if database is created, if not, stop all actions.
                dbcontextinteresting.Database.EnsureCreated();

                //
                //      Adding data to Top15InterestingGames that has many-to-many relationship with Genres
                //

                Top15interestinggames? FirstRowData = dbcontextinteresting.Top15interestinggames.FirstOrDefault();
                if (FirstRowData?.Updated == null)
                {
                    var top15InterestingGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                        query: "fields id, name, genres.name, first_release_date, url;" +
                        " sort follows desc;" +
                        " where follows != null & first_release_date >= " + unixFirst + ";" +
                        " limit 15; ");

                    foreach (Game game in top15InterestingGames)
                    {
                        var tmp = game.Genres.Values.ToList();

                        var ReleaseDate = game.FirstReleaseDate.Value;
                        //
                        var TIRecord = new Top15interestinggames()
                            //
                            {
                                GameId = game.Id,
                                Title = game.Name,
                                FirstReleaseDate = ReleaseDate.UtcDateTime,
                                Url = game.Url,
                                Updated = currentDate
                            };

                        foreach (Genre genre in tmp)
                        {
                            var genreFromDb = await dbcontextinteresting.Genres.FindAsync(genre.Id);
                            var TIGHG = new Top15interestinggamesHasGenres
                                {
                                    GenresGenre = genreFromDb,
                                    Top15interestinggamesGame = TIRecord
                                };
                            
                            TIRecord.Top15interestinggamesHG.Add(TIGHG);
                        }

                        await dbcontextinteresting.Set<Top15interestinggames>().AddAsync(TIRecord);
                    }
                    await dbcontextinteresting.SaveChangesAsync();
                }
                else
                {
                    DateTime LastUpdate = (DateTime)FirstRowData.Updated;
                    TimeSpan timeSpan = currentDate.Subtract(LastUpdate);

                    if (timeSpan.TotalDays > 6)
                    {
                        try
                        {
                            dbcontextinteresting.Database.ExecuteSqlRaw("DELETE FROM top15interestinggames_has_genres;"); // Working deletion of all data inside table.
                            await dbcontextinteresting.SaveChangesAsync();
                            dbcontextinteresting.Database.ExecuteSqlRaw("DELETE FROM top15interestinggames;");
                            await dbcontextinteresting.SaveChangesAsync();
                            dbcontextinteresting.Database.ExecuteSqlRaw("DELETE FROM genres;");
                            await dbcontextinteresting.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            throw new Exception("Failed truncating the tables.");
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        //First - check if database is created, if not, stop all actions.
                        dbcontextinteresting.Database.EnsureCreated();

                        //
                        //      Adding data to Top15InterestingGames that has many-to-many relationship with Genres
                        //

                        FirstRowData = dbcontextinteresting.Top15interestinggames.FirstOrDefault();
                        if (FirstRowData?.Updated == null)
                        {
                            var top15InterestingGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                                query: "fields id, name, genres.name, first_release_date, url;" +
                                " sort follows desc;" +
                                " where follows != null & first_release_date >= " + unixFirst + ";" +
                                " limit 15; ");

                            foreach (Game game in top15InterestingGames)
                            {
                                var tmp = game.Genres.Values.ToList();

                                var ReleaseDate = game.FirstReleaseDate.Value;
                                //
                                var TIRecord = new Top15interestinggames()
                                //
                                {
                                    GameId = game.Id,
                                    Title = game.Name,
                                    FirstReleaseDate = ReleaseDate.UtcDateTime,
                                    Url = game.Url,
                                    Updated = currentDate
                                };

                                foreach (Genre genre in tmp)
                                {
                                    var genreFromDb = await dbcontextinteresting.Genres.FindAsync(genre.Id);
                                    var TIGHG = new Top15interestinggamesHasGenres
                                    {
                                        GenresGenre = genreFromDb,
                                        Top15interestinggamesGame = TIRecord
                                    };

                                    TIRecord.Top15interestinggamesHG.Add(TIGHG);
                                }

                                await dbcontextinteresting.Set<Top15interestinggames>().AddAsync(TIRecord);
                            }
                            await dbcontextinteresting.SaveChangesAsync();
                        }
                    }
                }
            }
        }
    }
}
