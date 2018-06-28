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
			int campground = int.Parse(Console.ReadLine());

			Console.WriteLine("What is the arrival date? mm/dd/yyyy  ");
			DateTime startDate = DateTime.Parse(Console.ReadLine());

			Console.WriteLine("What is the departure date? mm/dd/yyyy  ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            ReservationHandler reservationHandler = new ReservationHandler(Park, campground, startDate, endDate);

            List<Site> availableSites = new List<Site>(reservationHandler.CheckAvailabilty());

            foreach (var availableSite in availableSites)
            {
                Console.WriteLine(availableSite.SiteID);
            }
		}
	}
}
