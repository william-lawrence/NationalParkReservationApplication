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

		public void DisplayParkInfo(string parkName)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("SELECT * FROM park WHERE name = @parkName", conn);
					cmd.Parameters.AddWithValue("@parkName", parkName);

					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Park park = new Park();

						park.Name = Convert.ToString(reader["name"]);
						park.Location = Convert.ToString(reader["location"]);
						park.EstDate = Convert.ToDateTime(reader["establish_date"]);
						park.Area = Convert.ToInt32(reader["area"]);
						park.Visitors = Convert.ToInt32(reader["visitors"]);
						park.Description = Convert.ToString(reader["description"]);

						Console.Clear();
						Console.WriteLine(park.Name);
						Console.WriteLine();
						Console.WriteLine($"Location: {park.Location}");
						Console.WriteLine($"Established: {park.EstDate.ToString("MM/dd/yyyy")}");
						Console.WriteLine($"Area: {park.Area.ToString()}");
						Console.WriteLine($"Annual Visitors: {park.Visitors.ToString()}");
						Console.WriteLine();
						Console.WriteLine(park.Description);
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
