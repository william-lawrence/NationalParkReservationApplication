using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
	public class BookingSubMenuCLI
	{
		public void DisplayBookingSubMenu()
		{
			Console.WriteLine();
			Console.WriteLine("Which campground (enter 0 to cancel)?  ");
			int campground = int.Parse(Console.ReadLine());

			Console.WriteLine("What is the arrival date? mm/dd/yyyy  ");
			DateTime startDate = DateTime.Parse(Console.ReadLine());

			Console.WriteLine("What is the departure date? mm/dd/yyyy  ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
		}
	}
}
