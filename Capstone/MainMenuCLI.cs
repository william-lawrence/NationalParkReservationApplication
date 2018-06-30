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
        const string Command_Quit = "Q";

        //connection string for the nation park database. This may need to be chnaged depending on the machine that is running it.
        const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

        //Displays the main menu.
        public void DisplayCLI()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Select a Park for Further Details");
                List<Park> parks = DisplayAllParks();

                Console.WriteLine("Q) Quit");
                //string selectedPark = Console.ReadLine();
                string userInput = Console.ReadLine().ToLower();
                ParkSqlDAL parkDAL = new ParkSqlDAL(DatabaseConnection);

                // Each of menu items go through the following flow for the selected park:
                // 1. Instantiate a new park object.
                // 2. Get all the infomation for that park from the database throught the DAL.
                // 3. Display the information for the park.
                // 4. Call and Display the Submenu.
                try
                {
                    switch (userInput.ToUpper())
                    {
                        case Command_Quit:
                            System.Environment.Exit(0);
                            break;

                        default:
                            if (!CheckIfParkExists(int.Parse(userInput), parks))
                            {
                                Console.WriteLine("Sorry, that park doesn't exist!");
                                System.Threading.Thread.Sleep(1500);
                                break;
                            }

                            Park park = new Park(int.Parse(userInput));
                            GetAllInfo(park);
                            DisplayParkInfo(park);
                            SubMenuCLI SubMenu = new SubMenuCLI(park);
                            SubMenu.DisplaySubMenuCLI();
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("That is not a valid input.");
                    System.Threading.Thread.Sleep(1500);
                }
            }
        }

        private bool CheckIfParkExists(int userInput, List<Park> parks)
        {
            bool parkExists = false;

            foreach (Park park in parks)
            {
                if (userInput == park.Id)
                {
                    parkExists = true;
                    break;
                }
            }

            return parkExists;
        }

        /// <summary>
        /// Displays the information for a given park and stores it in a list that.
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
        /// Displays all the parks that are in the database and storees them in a list.
        /// </summary>
        private List<Park> DisplayAllParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.GetAllParks();

            foreach (Park park in parks)
            {
                Console.WriteLine($"{park.Id.ToString()}) {park.Name}");
            }

            return parks;
        }

        /// <summary>
        /// Once a park is selected, goes to the data base and gets all the data
        /// for the park, its campgrounds and its sites. 
        /// This data shouldn't change super frequently.
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
        }
    }
}
