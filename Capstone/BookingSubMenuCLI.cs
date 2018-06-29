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

				Console.WriteLine("Results Matchin Your Search Criteria");
				Console.Write("Site No.".PadRight(10));
				Console.Write("Max Occup.".PadRight(15));
				Console.Write("Accesible?".PadRight(17));
				Console.Write("Max RV Length".PadRight(17));
				Console.Write("Utilities?".PadRight(10));
				Console.WriteLine("Cost".PadRight(10));

				foreach (var availableSite in availableSites)
				{
					Console.WriteLine($"{availableSite.SiteID.ToString().PadRight(9)} {availableSite.MaxOccupancy.ToString().PadRight(14)} {ToYesOrNo(availableSite.Accessible).ToString().PadRight(16)} {availableSite.MaxRVLength.ToString().PadRight(16)} {ToYesOrNo(availableSite.Utilities).ToString().PadRight(8)} {totalCost.ToString("C2")}");
				}

				VerifyingCampsite(ref reserving, ref siteNumber, availableSites);
				CreatingReservation(siteNumber, reservationHandler);

				running = false;
			}
		}

		private static void CreatingReservation(int siteNumber, ReservationHandlerDAL reservationHandler)
		{
			Console.WriteLine("What name should the reservation be made under? ");
			string name = Console.ReadLine();

			int confirmationId = reservationHandler.CreateReservation(siteNumber, name);

			Console.WriteLine($"The reservation has been made and the confirmation id is {confirmationId}.");
			System.Threading.Thread.Sleep(3000);
			Console.Clear();
		}

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
					catch (Exception)
					{
						Console.WriteLine("Sorry that is not a valid date format.");
						System.Threading.Thread.Sleep(1500);
					}
				}
			}
		}

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
