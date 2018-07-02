using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class BookingSubMenuCLI
    {
        //connection string for the nation park database. This may need to be chnaged depending on the machine that is running it.
        const string DatabaseConnection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

        public Park Park { get; set; }

        public BookingSubMenuCLI(Park park)
        {
            this.Park = park;
        }

        public void DisplayBookingSubMenu()
        {
            bool running = true;
            bool searching = true;
            bool reserving = true;
            int campgroundID = 0;
            int siteNumber = 0;
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            while (running)
            {
                VerifyingReservation(ref running, ref searching, ref campgroundID, ref startDate, ref endDate);

                Console.Clear();

                decimal totalCost = FindTotalCost(Park, campgroundID, startDate, endDate);

                ReservationHandlerDAL reservationHandler = new ReservationHandlerDAL(Park, campgroundID, startDate, endDate, DatabaseConnection);

                List<Site> availableSites = new List<Site>(reservationHandler.CheckAvailabilty(startDate, endDate));

                Console.WriteLine("Results Matching Your Search Criteria");
                Console.Write("Site No.".PadRight(10));
                Console.Write("Max Occup.".PadRight(15));
                Console.Write("Accesible?".PadRight(17));
                Console.Write("Max RV Length".PadRight(17));
                Console.Write("Utilities?".PadRight(12));
                Console.WriteLine("Cost".PadRight(10));

                foreach (var availableSite in availableSites)
                {
                    Console.WriteLine($"{availableSite.SiteID.ToString().PadRight(9)} " +
                        $"{availableSite.MaxOccupancy.ToString().PadRight(14)} " +
                        $"{ToYesOrNo(availableSite.Accessible).ToString().PadRight(16)} " +
                        $"{ZeroToNA(availableSite.MaxRVLength).ToString().PadRight(16)} " +
                        $"{ToYesOrNo(availableSite.Utilities).ToString().PadRight(11)} " +
                        $"{totalCost.ToString("C2")}");
                }

                VerifyingCampsite(ref reserving, ref siteNumber, availableSites);
                CreatingReservation(siteNumber, reservationHandler);

                MainMenuCLI mainMenu = new MainMenuCLI();
                mainMenu.DisplayCLI();
            }
        }

        /// <summary>
        /// Creates the reservation.
        /// </summary>
        /// <param name="siteNumber">The number of the site to be reserved.</param>
        /// <param name="reservationHandler">The DAL that is interfacing with the national park database to make the reservation.</param>
		private static void CreatingReservation(int siteNumber, ReservationHandlerDAL reservationHandler)
        {
            Console.WriteLine("What name should the reservation be made under? ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Sorry, your name must consist of 1 or more letters!");
                CreatingReservation(siteNumber, reservationHandler);
            }

            int confirmationId = reservationHandler.CreateReservation(siteNumber, name);

            Console.WriteLine($"The reservation has been made and the confirmation id is {confirmationId}.");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
        }

        /// <summary>
        /// The menu where the reservation is made.
        /// </summary>
        /// <param name="reserving">The bool that represents if the menu is running.</param>
        /// <param name="siteNumber">The number of the sight to be reseved.</param>
        /// <param name="availableSites">The list of sites that are available to be reserved.</param>
		private void VerifyingCampsite(ref bool reserving, ref int siteNumber, List<Site> availableSites)
        {
            while (reserving)
            {
                Console.Write("Which site should be reserved (enter 0 to cancel)? ");
                siteNumber = int.Parse(Console.ReadLine());

                if (siteNumber == 0)
                {
                    break;
                }
                else if (!CheckIfCampsiteAvailable(availableSites, siteNumber))
                {
                    Console.WriteLine("Sorry, that campsite is not a valid choice!");
                }
                else if (CheckIfCampsiteAvailable(availableSites, siteNumber))
                {
                    reserving = false;
                }

            }
        }

        /// <summary>
        /// the overall process for the reservation making process. 
        /// </summary>
        /// <param name="running">The bool representing if the booking menu cli is running.</param>
        /// <param name="searching">The bool representing if the user is in the process of searching for a park.</param>
        /// <param name="campgroundID">The id of the campground where the reservation is being made.</param>
        /// <param name="startDate">The date when the reservation begin.</param>
        /// <param name="endDate">The date when the reservation ends.</param>
		private void VerifyingReservation(ref bool running, ref bool searching, ref int campgroundID, ref DateTime startDate, ref DateTime endDate)
        {
            while (searching)
            {
                Console.Clear();
                Console.WriteLine();
                DisplayAllCampGrounds(Park);
                Console.WriteLine();
                Console.Write("Which campground (enter 0 to cancel)?  ");
                campgroundID = int.Parse(Console.ReadLine());

                if (campgroundID == 0)
                {
                    running = false;
                    break;
                }
                else if (!CheckIfCampgroundInPark(Park, campgroundID))
                {
                    Console.WriteLine("Sorry, that campground is not a valid choice!");
                    System.Threading.Thread.Sleep(1500);
                }
                else
                {
                    try
                    {
                        Console.Write("What is the arrival date? mm/dd/yyyy  ");
                        startDate = DateTime.Parse(Console.ReadLine());

                        Console.Write("What is the departure date? mm/dd/yyyy  ");
                        endDate = DateTime.Parse(Console.ReadLine());
                        if (startDate >= DateTime.Now)
                        {
                            if ((endDate - startDate).TotalDays < 0)
                            {
                                Console.WriteLine("Sorry, time doesn't work that way!");
                                System.Threading.Thread.Sleep(1500);
                            }
                            else
                            {
                                searching = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("You can't arrive before today!");
                            System.Threading.Thread.Sleep(1500);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Sorry that is not a valid date format.");
                        System.Threading.Thread.Sleep(1500);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a selected site selected is a valid.
        /// </summary>
        /// <param name="availableSites">The list of available sites.</param>
        /// <param name="siteNumber">The number of the site selected.</param>
        /// <returns></returns>
		private bool CheckIfCampsiteAvailable(List<Site> availableSites, int siteNumber)
        {
            bool validSite = false;

            foreach (var site in availableSites)
            {
                if (site.SiteID == siteNumber)
                {
                    validSite = true;
                    break;
                }
            }
            return validSite;
        }

        /// <summary>
        /// Determines if the campground ID is in the selected park.
        /// </summary>
        /// <param name="park">The park that the user is trying to make the reservation in.</param>
        /// <param name="campgroundId">The campground where the person is trying to make the reservation.</param>
        /// <returns></returns>
        private bool CheckIfCampgroundInPark(Park park, int campgroundId)
        {
            bool validCampground = false;

            foreach (var campground in Park.Campgrounds)
            {
                if (campground.CampgroundID == campgroundId)
                {
                    validCampground = true;
                    break;
                }
            }
            return validCampground;
        }

        /// <summary>
        /// Determines the cost to stay at a given park for a given number of days. 
        /// </summary>
        /// <param name="park">The park where the camppsite is that the customer is choosin to stay at.</param>
        /// <param name="campgroundID">The id of the campground in the national park database.</param>
        /// <param name="startDate">The start date of the reservation.</param>
        /// <param name="endDate">The end date of the reservation.</param>
        /// <returns></returns>
		private decimal FindTotalCost(Park park, int campgroundID, DateTime startDate, DateTime endDate)
        {
            decimal totalCost = 0;

            foreach (var site in Park.Campgrounds)
            {
                if (site.CampgroundID == campgroundID)
                {
                    totalCost = ((decimal)(endDate - startDate).TotalDays * site.DailyFee);
                    break;
                }
            }

            return totalCost;
        }

        /// <summary>
        /// Converts a bool to yes or no.
        /// </summary>
        /// <param name="value">true = yes, false = no. </param>
        /// <returns></returns>
        public string ToYesOrNo(bool value)
        {
            return value ? "Yes" : "No";
        }

        /// <summary>
        /// Converts an int to a string, changes 0 to N/A
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string ZeroToNA(int length)
        {
            string stringLength = "";

            if (length == 0)
            {
                stringLength = "N/A";
            }
            else
            {
                stringLength = length.ToString();
            }

            return stringLength;
        }

        /// <summary>
        /// Displays all the campgrounds in a given park.
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
            //Console.WriteLine($"Name Open Close Daily Fee");

            //foreach (Campground campground in park.Campgrounds)
            //{
            //	Console.WriteLine($"#{campground.CampgroundID} {campground.Name} {campground.OpeningMonth} {campground.ClosingMonth} {campground.DailyFee.ToString("C2")}");
            //}

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
