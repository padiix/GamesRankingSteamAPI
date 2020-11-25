using IGDB;
using IGDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRankingSteamAPI.Controllers
{
    public class IGDBAPIController
    {
        public async void ConnectToDatabase()
        {
            IGDBClient igdb;
            try
            {
                igdb = new IGDBClient(
                    // Found in Twitch Developer portal for your app
                    Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
                    Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET")
                );
            }
            catch (Exception er)
            {
                throw er;
            }
            var top10PopularGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                query: "fields id, age_ratings.category, age_ratings.rating, category, first_release_date, rating, rating_count, genres.name, name, summary, url; "
                        + "sort rating_count desc; "
                        + "where(rating > 60) & (rating_count > 8) & (hypes != null) & (category = 0) & (first_release_date >= 1598918401) & (first_release_date < 1604275199); "
                        + "limit 10;");
            
            var top15InterestingGames = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
                query: "fields id, name, summary, genres.name, age_ratings.rating, first_release_date, total_rating, follows, url; "
                       + "sort follows desc; "
                       + "where follows != null & first_release_date >= 1577836800; "
                       + "limit 15; ");

            //2505602 - wartość równego miesiąca w Unix Time Stamp
            //5356798 - wartość miesiąca od 1. (00:00:01) do 31. (23:59:59) w Unix Time Stamp

        }
    }
}
