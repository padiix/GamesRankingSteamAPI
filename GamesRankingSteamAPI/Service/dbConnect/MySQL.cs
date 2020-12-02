using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRankingSteamAPI.Service
{
    public class MySQL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
        MySqlConnection connection;
        void Connect() {
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch(Exception er)
            {
                Console.WriteLine("Failed to connect to database due to: " + er.ToString());
            }
        }

        void Disconnect()
        {
            using (connection)
            {
                try
                {
                    connection.Close();
                }
                catch (Exception er)
                {
                    Console.WriteLine("Failed to disconnect from database due to: " + er.ToString());
                }
            }
        }
    }
}
