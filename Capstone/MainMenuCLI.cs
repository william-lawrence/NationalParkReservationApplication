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
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Select a Park for Further Details");
                DisplayAllParks();
                Console.WriteLine("Q) Quit");
                string command = Console.ReadLine();
                ParkSqlDAL parkDAL = new ParkSqlDAL(DatabaseConnection);

                // Each of menu items go through the following flow for the selected park:
                // 1. Instantiate a new park object.
                // 2. Get all the infomation for that park from the database throught the DAL.
                // 3. Display the inforamtion for the park.
                // 4. Call and Display the Submenu.
                switch (command.ToUpper())
                {
                    case Command_ViewAcadia:
                        Park acadia = new Park(1);
                        GetAllInfo(acadia);
                        DisplayParkInfo(acadia);
                        SubMenuCLI acadiaSubMenu = new SubMenuCLI(acadia);
                        acadiaSubMenu.DisplaySubMenuCLI();
                        break;

                    case Command_ViewArches:
                        Park arches = new Park(2);
                        GetAllInfo(arches);
                        DisplayParkInfo(arches);
                        SubMenuCLI archesSubMenu = new SubMenuCLI(arches);
                        archesSubMenu.DisplaySubMenuCLI();
                        break;

                    case Command_ViewCuyahogaNationalValley:
                        Park cuyahogaValley = new Park(3);
                        GetAllInfo(cuyahogaValley);
                        SubMenuCLI cuyahogaSubMenu = new SubMenuCLI(cuyahogaValley);
                        cuyahogaSubMenu.DisplaySubMenuCLI();
                        break;

                    case Command_Quit:
                        System.Environment.Exit(0);
                        break;

					default:
						Console.WriteLine();
						Console.WriteLine("Sorry, that's not a valid choice!");
						System.Threading.Thread.Sleep(1500);
						break;
                }
            }
        }

        /// <summary>
        /// Displays the information for a given park.
        /// </summary>
        /// <param name="park">The park object that you want to display get information for.</param>
        private void DisplayParkInfo(Park park)
        {
			Console.Clear();
			Console.WriteLine();
            Console.WriteLine($"{park.Name}");
            Console.WriteLine($"Location: {park.Location}");
            Console.WriteLine($"Established: {park.EstDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"Area: {park.Area.ToString("N0")} sq km");
            Console.WriteLine($"Annual Visitors: {park.Visitors.ToString("N0")}");
            Console.WriteLine();
            Console.WriteLine(park.Description);
        }

        /// <summary>
        /// Displays all the parks that are in the database.
        /// </summary>
        private void DisplayAllParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            IList<Park> parks = dal.GetAllParks();

            foreach (Park park in parks)
            {
                Console.WriteLine($"{park.Id.ToString()}) {park.Name}");
            }
        }

        /// <summary>
        /// Once a park is selected, goes to the data base and gets all the data
        /// for the park, its campgrouds, its cites, and its reservations. 
        /// </summary>
        /// <param name="park"></param>
        private void GetAllInfo(Park park)
        {
            ParkSqlDAL parkDAL = new ParkSqlDAL(DatabaseConnection);
            parkDAL.GetParkInfo(park);

             CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(DatabaseConnection);
            campgroundDAL.GetCampgroundInfo(park);

            SiteSqlDAL siteDAL = new SiteSqlDAL(DatabaseConnection);
            siteDAL.GetSiteInfo(park);

            //shh. ;)

            //ReservationSqlDAL reservationDAL = new ReservationSqlDAL(DatabaseConnection);
            //foreach (var campground in park.Campgrounds)
            //{
            //    reservationDAL.GetReservationInfo(campground);
            //}
        }
    }
}
