using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public int SiteID { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }

        public Reservation(int siteId, string name, DateTime startDate, DateTime endDate)
        {
            this.SiteID = siteId;
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            //this.CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Reservation()
        {

        }
    }


}
