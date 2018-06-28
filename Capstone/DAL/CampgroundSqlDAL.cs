using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL
    {
        private string ConnectionString;

        public CampgroundSqlDAL(string dbConnectionString)
        {
            this.ConnectionString = dbConnectionString;
        }

        /// <summary>
        /// Gets all the campgrounds and stores them as a list in the park object if they are in that park.
        /// </summary>
        /// <param name="parkID"></param>
        /// <returns></returns>
		public void GetCampgroundInfo(Park park)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @ID;", conn);
                    cmd.Parameters.AddWithValue("ID", park.Id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Reading through all the rows and collecting each campgrounds information in the park.
                    while (reader.Read())
                    {
                        Campground campground = new Campground
                        {
                            ParkID = Convert.ToInt32(reader["park_id"]),
                            CampgroundID = Convert.ToInt32(reader["campground_id"]),
                            Name = Convert.ToString(reader["name"]),
                            OpeningMonth = Convert.ToInt32(reader["open_from_mm"]),
                            ClosingMonth = Convert.ToInt32(reader["open_to_mm"]),
                            DailyFee = Convert.ToDecimal(reader["daily_fee"])
                        };

                        // Adding the campground to the list of campgrounds in the park object.
                        park.Campgrounds.Add(campground);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
