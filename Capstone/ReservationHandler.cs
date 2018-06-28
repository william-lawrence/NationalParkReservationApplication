using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class ReservationHandler
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int CampgroundID { get; set; }
        public Park Park { get; set; }

        public ReservationHandler(Park park, int campgroundID, DateTime start, DateTime end)
        {
            this.Park = park;
            this.CampgroundID = campgroundID;
            this.Start = start;
            this.End = end;
        }

        public List<Site> CheckAvailabilty()
        {
            DateRange requestedRange = new DateRange(this.Start, this.End);

            List<Site> availableSites = new List<Site>();

            Campground selectedCampground = new Campground();

            foreach (var campground in Park.Campgrounds)
            {
                if (campground.CampgroundID == this.CampgroundID)
                {
                    selectedCampground = campground;
                }
            }

            foreach (var campsite in selectedCampground.Sites)
            {
                foreach (var reservation in campsite.Reservations)
                {
                    DateTime reservationsStartDate = reservation.StartDate;
                    DateTime reservationEndDate = reservation.EndDate;
                    DateRange reservationRange = new DateRange(reservationsStartDate, reservationEndDate);

                    if (!(reservationRange.Includes(requestedRange)))
                    {
                        availableSites.Add(campsite);
                    }
                }
            }

            return availableSites;
        }


    }
}
