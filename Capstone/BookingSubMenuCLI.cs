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
        public Park Park { get; set; }

        public BookingSubMenuCLI(Park park)
        {
            this.Park = park;
        }

		public void DisplayBookingSubMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Which campground (enter 0 to cancel)?  ");
            int campgroundID = int.Parse(Console.ReadLine());


            Console.WriteLine("What is the arrival date? mm/dd/yyyy  ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("What is the departure date? mm/dd/yyyy  ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            decimal totalCost = FindTotalCost(Park, campgroundID, startDate, endDate);

            ReservationHandler reservationHandler = new ReservationHandler(Park, campgroundID, startDate, endDate);

            List<Site> availableSites = new List<Site>(reservationHandler.CheckAvailabilty());

            foreach (var availableSite in availableSites)
            {
                Console.WriteLine($"{availableSite.SiteID} {availableSite.MaxOccupancy} {ToYesOrNo(availableSite.Accessible)} {availableSite.MaxRVLength} {ToYesOrNo(availableSite.Utilities)} {totalCost.ToString("C2")}");
            }
        }

        private decimal FindTotalCost(Park park, int campgroundID, DateTime startDate, DateTime endDate)
        {
            decimal totalCost = 0;

            foreach(var site in Park.Campgrounds)
            {
                if(site.CampgroundID == campgroundID)
                {
                    totalCost = ((decimal)(endDate - startDate).TotalDays * site.DailyFee);
                    break;
                }
            }

            return totalCost;
        }

        public string ToYesOrNo (bool value)
        {
            return value ? "Yes" : "No";
        }
	}
}
