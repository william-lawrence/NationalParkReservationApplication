using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;

namespace Capstone.Models
{
    public class Park
    {
        /// <summary>
        /// The ID of the park as reflected in the park database.
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        /// The name of the park.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// The state that the park primarily resides in.
        /// </summary>
		public string Location { get; set; }

        /// <summary>
        /// The date that the park was officially established.
        /// </summary>
		public DateTime EstDate { get; set; }

        /// <summary>
        /// The area of the park in square kilometers.
        /// </summary>
		public int Area { get; set; }

        /// <summary>
        /// The number of visitors that go to a park annually. 
        /// </summary>
		public int Visitors { get; set; }

        /// <summary>
        /// The description of the park.
        /// </summary>
		public string Description { get; set; }

        /// <summary>
        /// A list of all the campgrounds in a give park.
        /// </summary>
		public List<Campground> Campgrounds { get; set; }

        public Park(int id)
        {
            this.Id = id;
            this.Campgrounds = new List<Campground>();
        }

        public Park()
        { }
    }


}
