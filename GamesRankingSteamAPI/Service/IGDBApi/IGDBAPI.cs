using IGDB;
using IGDB.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
            long unixLastFixed = unixFirst + 5356798;

            IGDBClient igdb = new IGDBClient(// Found in Twitch Developer portal for your app
                    Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
                    Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET"));
            
            Game[] top10PopularGames;
            try
            {
                top10PopularGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                    query: "fields id, age_ratings.category, age_ratings.rating, category, first_release_date, rating, rating_count, genres.name, name, summary, url; "
                            + "sort rating_count desc; "
                            + "where(rating > 60) & (rating_count > 8) & (hypes != null) & (category = 0) & (first_release_date >= " + unixFirst + ") & (first_release_date < " + unixLast + "); "
                            + "limit 10;");
            }
            catch (Exception er)
            {
                Console.WriteLine("Error while fetching data for 'Top 10 Popular Games' caused by: " + er.ToString());
            }

            Game[] top15InterestingGames;
            try { 
                top15InterestingGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                    query: "fields id, name, summary, genres.name, age_ratings.rating, first_release_date, total_rating, follows, url; "
                            + "sort follows desc; "
                            + "where follows != null & first_release_date >= "+ unixFirst +"; "
                            + "limit 15; ");
            }
            catch (Exception er)
            {
                Console.WriteLine("Error while fetching data for 'Top 15 Interesting Games' caused by: " + er.ToString());
            }

            //2505602 - wartość równego miesiąca w Unix Time Stamp
            //5356798 - wartość miesiąca od 1. (00:00:01) do 31. (23:59:59) w Unix Time Stamp


        }
    }
}
