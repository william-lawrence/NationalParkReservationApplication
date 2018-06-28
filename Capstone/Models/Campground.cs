using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        /// <summary>
        /// Campground ID in the data base
        /// </summary>
        public int CampgroundID { get; set; }

        /// <summary>
        /// ID for the park that the campground resides in.
        /// </summary>
        public int ParkID { get; set; }

        /// <summary>
        /// Name of the campground
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Month that the campground opens represented as an int (1 - 12).
        /// </summary>
        public int OpeningMonth { get; set; }

        /// <summary>
        /// Month that the campground closes represented as an int (1 - 12).
        /// </summary>
        public int ClosingMonth { get; set; }

        /// <summary>
        /// The daily fee to use a particular campground.
        /// </summary>
        public decimal DailyFee { get; set; }

        /// <summary>
        /// List of all the cites in a campground
        /// </summary>
        public List<Site> Sites { get; set; }


        public Campground()
        {
            //creating an empty list that we can add campgrounds to. 
            this.Sites = new List<Site>();
        }

    }
}
