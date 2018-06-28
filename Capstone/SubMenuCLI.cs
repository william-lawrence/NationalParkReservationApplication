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
        const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

        public SubMenuCLI(Park park)
        {
            this.Park = park;
        }

        public void DisplaySubMenuCLI()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Select a Command");
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Search for Reservation");
                Console.WriteLine("3) Return to Previous Screen");
                string command = Console.ReadLine();

                CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(DatabaseConnection);

                switch (command)
                {
                    case Command_ViewCampgrounds:
                        DisplayAllCampGrounds(Park);
                        break;

                    case Command_SearchForReservation:
                        BookingSubMenuCLI submenu = new BookingSubMenuCLI();
                        submenu.DisplayBookingSubMenu();
                        break;
                }
            }
        }

        private static void DisplayAllCampGrounds(Park park)
        {
            Console.WriteLine($"Name Open Close Daily Fee");

            foreach (Campground campground in park.Campgrounds)
            {
                Console.WriteLine($"#{campground.CampgroundID} {campground.Name} {campground.OpeningMonth} {campground.ClosingMonth} {campground.DailyFee.ToString("C2")}");
            }

        }
    }
}
