using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    /// <summary>
    /// Class representing a campsite
    /// </summary>
	public class Site
	{
        /// <summary>
        /// The ID of a given site in a campground.
        /// </summary>
		public int SiteID { get; set; }

        /// <summary>
        /// The ID of the campground where the site is located.
        /// </summary>
		public int CampgroundID { get; set; }

        /// <summary>
        /// The number that represents the site. 
        /// </summary>
		public int SiteNumber { get; set; }

        /// <summary>
        /// The max occupancy of a given campsite.
        /// </summary>
		public int MaxOccupancy { get; set; }

        /// <summary>
        /// A bool representing if a the park is accessible.
        /// </summary>
		public bool Accessible { get; set; }

        /// <summary>
        /// The max length of an RV that can be brouhg to the campsite.
        /// </summary>
		public int MaxRVLength { get; set; }

        /// <summary>
        /// A bool representing if the park has access to utilities. 
        /// </summary>
		public bool Utilities { get; set; }
	}
}
