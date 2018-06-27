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
		private string connectionString;

		public ReservationSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		public List<Reservation> GetAllReservations(int campgroundID)
		{
			List<Reservation> reservations = new List<Reservation>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("SELECT * FROM reservation WHERE id = @campgroundID;", conn);
					cmd.Parameters.AddWithValue("@campgroundID", campgroundID);

					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Reservation reservation = new Reservation();

						reservation.ReservationID = Convert.ToInt32(reader["reservation_id"]);
						reservation.SiteID = Convert.ToInt32(reader["site_id"]);
						reservation.Name = Convert.ToString(reader["name"]);
						reservation.StartDate = Convert.ToDateTime(reader["from_date"]);
						reservation.EndDate = Convert.ToDateTime(reader["to_date"]);
					}
				}

			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return reservations;
		}
	}
}
