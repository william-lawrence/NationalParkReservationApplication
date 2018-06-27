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
		private string connectionString;

		public SiteSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		public List<Site> GetAllSites()
		{
			List<Site> sites = new List<Site>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("SELECT * FROM site;", conn);

					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Site site = new Site();

						site.SiteID = Convert.ToInt32(reader["site_id"]);
						site.CampgroundID = Convert.ToInt32(reader["campground_id"]);
						site.SiteNumber = Convert.ToInt32(reader["site_number"]);
						site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
						site.Accessible = Convert.ToBoolean(reader["accessible"]);
						site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
						site.Utilities = Convert.ToBoolean(reader["utilities"]);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return sites;
		}
	}
}
