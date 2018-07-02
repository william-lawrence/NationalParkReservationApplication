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

        //connection string for the nation park database. This may need to be chnaged depending on the machine that is running it.
        const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

        public SubMenuCLI(Park park)
        {
            this.Park = park;
        }

        /// <summary>
        /// Displays the menu after a park has been selected. 
        /// </summary>
        public void DisplaySubMenuCLI()
        {
            //The menu is "running till they are done with that menu"
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

		/// <summary>
		/// Displays all campgrounds
		/// </summary>
		/// <param name="park"></param>
		private static void DisplayAllCampGrounds(Park park)
        {
			Console.Write("Name".PadLeft(7).PadRight(26));
			Console.Write("Open".PadLeft(6));
			Console.Write("Close".PadLeft(11));
			Console.WriteLine("Daily Fee".PadLeft(18));

            foreach (Campground campground in park.Campgrounds)
            {
				Console.WriteLine("#{0,-2}{1,-25}{2,-10}{3,-10}{4,10}",
					campground.CampgroundID,
					campground.Name,
					ToMonthName(campground.OpeningMonth),
					ToMonthName(campground.ClosingMonth),
					campground.DailyFee.ToString("C2"));
            }

        }
        
        /// <summary>
        /// Converts the numerical reprentation of a month to the string name 1 = "January" and so on.
        /// </summary>
        /// <param name="month">The numerical representation of the mom</param>
        /// <returns></returns>
		private static string ToMonthName(int month)
		{
			DateTime date = new DateTime(2018, month, 1);
			return date.ToString("MMMM");
		}
	}
}
