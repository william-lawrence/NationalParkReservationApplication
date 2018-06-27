using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
	public class ParkSqlDAL
	{
		private string connectionString;

		public ParkSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		public IList<Park> GetAllParks()
		{
			IList<Park> output = new List<Park>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("SELECT * FROM park;", conn);

					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Park park = new Park();

						park.Id = Convert.ToInt32(reader["park_id"]);
						park.Name = Convert.ToString(reader["name"]);
						park.Location = Convert.ToString(reader["location"]);
						park.EstDate = Convert.ToDateTime(reader["establish_date"]);
						park.Area = Convert.ToInt32(reader["area"]);
						park.Visitors = Convert.ToInt32(reader["visitors"]);
						park.Description = Convert.ToString(reader["description"]);

						output.Add(park);
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
