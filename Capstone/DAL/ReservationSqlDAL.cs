using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string ConnectionString;

        public ReservationSqlDAL(string dbConnectionString)
        {
            this.ConnectionString = dbConnectionString;
        }

        public void GetReservationInfo(Campground campground)
        {
            foreach (var site in campground.Sites)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * FROM reservation WHERE site_number = @siteID;", conn);
                        cmd.Parameters.AddWithValue("@siteID", site.SiteID);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Reservation reservation = new Reservation
                            {
                                ReservationID = Convert.ToInt32(reader["reservation_id"]),
                                SiteID = Convert.ToInt32(reader["site_id"]),
                                Name = Convert.ToString(reader["name"]),
                                StartDate = Convert.ToDateTime(reader["from_date"]),
                                EndDate = Convert.ToDateTime(reader["to_date"])
                            };

                            site.Reservations.Add(reservation);
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
