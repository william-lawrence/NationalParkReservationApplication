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
        public Park Park { get; set; }

        const string Command_ViewCampgrounds = "1";
        const string Command_SearchForReservation = "2";
        const string Command_Return = "3";
		bool running = true;
        const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

        public SubMenuCLI(Park park)
        {
            this.Park = park;
        }

        public void DisplaySubMenuCLI()
        {
            while (running)
			{
				Console.WriteLine();
				PrintCommands();
				string command = Console.ReadLine();

				CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(DatabaseConnection);

				switch (command)
				{
					case Command_ViewCampgrounds:
						Console.Clear();
						DisplayAllCampGrounds(Park);
						break;

					case Command_SearchForReservation:
						BookingSubMenuCLI submenu = new BookingSubMenuCLI(this.Park);
						submenu.DisplayBookingSubMenu();
						break;

					case Command_Return:
						running = false;
						break;

					default:
						Console.WriteLine();
						Console.WriteLine("Sorry, that's not a valid choice!");
						System.Threading.Thread.Sleep(1500);
						break;
				}
			}
		}

		private static void PrintCommands()
		{
			Console.WriteLine("Select a Command");
			Console.WriteLine("1) View Campgrounds");
			Console.WriteLine("2) Search for Reservation");
			Console.WriteLine("3) Return to Previous Screen");
		}

		private static void DisplayAllCampGrounds(Park park)
        {
			Console.Write("Name".PadLeft(3).PadRight(20));
			Console.Write("Open".PadRight(10));
			Console.Write("Close".PadRight(10));
			Console.WriteLine("Daily Fee");

            foreach (Campground campground in park.Campgrounds)
            {
                Console.WriteLine($"#{campground.CampgroundID} {campground.Name} {ToMonthName(campground.OpeningMonth)} {ToMonthName(campground.ClosingMonth)} {campground.DailyFee.ToString("C2")}");
            }

        }

		private static string ToMonthName(int month)
		{
			DateTime date = new DateTime(2018, month, 1);
			return date.ToString("MMMM");
		}
	}
}
