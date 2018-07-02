using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSqlDAL
    {
        private string ConnectionString;

        public SiteSqlDAL(string dbConnectionString)
        {
            this.ConnectionString = dbConnectionString;
        }

        public void GetSiteInfo(Park park)
        {
            foreach (var campground in park.Campgrounds)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * FROM site WHERE campground_id = @id;", conn);
                        cmd.Parameters.AddWithValue("@ID", campground.CampgroundID);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Site site = new Site
                            {
                                SiteID = Convert.ToInt32(reader["site_id"]),
                                CampgroundID = Convert.ToInt32(reader["campground_id"]),
                                SiteNumber = Convert.ToInt32(reader["site_number"]),
                                MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]),
                                Accessible = Convert.ToBoolean(reader["accessible"]),
                                MaxRVLength = Convert.ToInt32(reader["max_rv_length"]),
                                Utilities = Convert.ToBoolean(reader["utilities"])
                            };

                            campground.Sites.Add(site);
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
}
