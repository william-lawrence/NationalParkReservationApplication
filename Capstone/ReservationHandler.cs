using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class ReservationHandler
    {
        private string ConnectionString;

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int CampgroundID { get; set; }
        public Park Park { get; set; }

        public ReservationHandler(Park park, int campgroundID, DateTime start, DateTime end, string dbConnectionString)
        {
            this.Park = park;
            this.CampgroundID = campgroundID;
            this.Start = start;
            this.End = end;
            ConnectionString = dbConnectionString;
        }

        public int CreateReservation(int siteNumber, string name)
        {
            int confirmationId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation(site_id, name, from_date, to_date, create_date) VALUES (@siteNumber, @name, @start, @end, @create);", conn);
                    cmd.Parameters.AddWithValue("@siteNumber", siteNumber);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@start", this.Start);
                    cmd.Parameters.AddWithValue("@end", this.End);
                    cmd.Parameters.AddWithValue("@create", DateTime.Now);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(reservation_id) FROM reservation", conn);
                    confirmationId = Convert.ToInt32(cmd.ExecuteScalar());
                 }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return confirmationId;
        }

        public List<Site> CheckAvailabilty()
        {
            DateRange requestedRange = new DateRange(this.Start, this.End);

            List<Site> availableSites = new List<Site>();

            Campground selectedCampground = new Campground();

            foreach (var campground in Park.Campgrounds)
            {
                if (campground.CampgroundID == this.CampgroundID)
                {
                    selectedCampground = campground;
                }
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT site_id  FROM site WHERE site.site_id NOT IN " +
                        "(SELECT reservation.site_id FROM reservation WHERE @StartDate BETWEEN reservation.from_date " +
                        "AND reservation.to_date OR @EndDate BETWEEN reservation.from_date " +
                        "AND reservation.to_date) AND @campgroundId = site.campground_id;", conn);

                    cmd.Parameters.AddWithValue("@StartDate", this.Start);
                    cmd.Parameters.AddWithValue("@EndDate", this.End);
                    cmd.Parameters.AddWithValue("@campgroundId", selectedCampground.CampgroundID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int siteId = Convert.ToInt32(reader["site_id"]);
                        foreach (var campsite in selectedCampground.Sites)
                        {
                            if (siteId == campsite.SiteID)
                            {
                                availableSites.Add(campsite);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return availableSites;
        }
    }
}
