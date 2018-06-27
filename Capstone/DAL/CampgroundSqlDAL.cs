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
		private string connectionString;

		public CampgroundSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		public IList<Campground> GetAllCampgrounds(int parkID)
		{
			IList<Campground> output = new List<Campground>();

			int thisParkID = parkID;

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("SELECT * FROM campground;", conn);
					cmd.Parameters.AddWithValue("@thisPark_id", thisParkID);

					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Campground campground = new Campground();

						campground.ParkID = Convert.ToInt32(reader["park_id"]);
						campground.CampgroundID = Convert.ToInt32(reader["campground_id"]);
						campground.Name = Convert.ToString(reader["name"]);
						campground.OpeningMonth = Convert.ToInt32(reader["open_from_mm"]);
						campground.ClosingMonth = Convert.ToInt32(reader["open_to_mm"]);
						campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);

						output.Add(campground);
					}

					foreach (var campground in output)
					{
						if (campground.ParkID == parkID)
						{
							Console.WriteLine($"#{campground.CampgroundID} {campground.Name} {campground.OpeningMonth} {campground.ClosingMonth} {campground.DailyFee.ToString("C2")}");
						}
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return output;
		}
	}
}
