using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
	public class SubMenuCLI
	{
		//public int ParkID { get; set; }

		const string Command_ViewCampgrounds = "1";
		const string Command_SearchForReservation = "2";
		const string Command_Return = "3";
		const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

		//public SubMenuCLI(int id)
		//{
		//	this.ParkID = id;
		//}

		public void DisplaySubMenuCLI(int parkId)
		{
			while (true)
			{
				Console.WriteLine();
				Console.WriteLine("Select a Command");
				Console.WriteLine("1) View Campgrounds");
				Console.WriteLine("2) Search for Reservation");
				Console.WriteLine("3) Return to Previous Screen");
				string command = Console.ReadLine();

				CampgroundSqlDAL campground = new CampgroundSqlDAL(DatabaseConnection);

				switch (command)
				{
					case Command_ViewCampgrounds:
						campground.GetAllCampgrounds(parkId);
						break;
				}
			}
		}
	}
}
