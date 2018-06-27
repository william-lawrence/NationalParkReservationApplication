using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone
{
	public class MainMenuCLI
	{
		const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

		public void DisplayCLI()
		{
			while (true)
			{
				Console.WriteLine();
				Console.WriteLine("Select a Park for Further Details");
				
			}
		}

		private void GetAllParks()
		{
			ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
			IList<Park> parks = dal.GetAllParks();

			foreach (Park park in parks)
			{
				Console.WriteLine($"{park.Id.ToString()}) {park.Name}");
			}
		}
	}
}
