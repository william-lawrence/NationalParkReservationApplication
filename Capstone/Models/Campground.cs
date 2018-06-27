using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
     public class Campground
    {
        public int CampgroundID { get; set; }

        public int ParkID { get; set; } 

        public string Name { get; set; }

        public int OpeningMonth { get; set; }

        public int ClosingMonth { get; set; }

        public decimal DailyFee { get; set; }
    }
}
