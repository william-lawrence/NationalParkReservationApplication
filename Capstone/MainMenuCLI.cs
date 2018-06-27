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
		const string Command_ViewAcadia = "1";
		const string Command_ViewArches = "2";
		const string Command_ViewCuyahogaNationalValley = "3";
		const string Command_Quit = "Q";
		const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

		public void DisplayCLI()
		{
			while (true)
			{
				Console.WriteLine();
				Console.WriteLine("Select a Park for Further Details");
				DisplayAllParks();
				Console.WriteLine("Q) Quit");
				string command = Console.ReadLine();
				ParkSqlDAL park = new ParkSqlDAL(DatabaseConnection);

				switch (command.ToUpper())
				{
					case Command_ViewAcadia:
						park.DisplayParkInfo("Acadia");
						SubMenuCLI acadiaSubMenu = new SubMenuCLI();
						acadiaSubMenu.DisplaySubMenuCLI(1);
						break;

					case Command_ViewArches:
						park.DisplayParkInfo("Arches");
						SubMenuCLI archesSubMenu = new SubMenuCLI();
						archesSubMenu.DisplaySubMenuCLI(2);
						break;

					case Command_ViewCuyahogaNationalValley:
						park.DisplayParkInfo("Cuyahoga Valley");
						SubMenuCLI cuyahogaSubMenu = new SubMenuCLI();
						cuyahogaSubMenu.DisplaySubMenuCLI(3);
						break;
				}
			}
		}

		private void DisplayAllParks()
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
