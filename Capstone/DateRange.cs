using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class DateRange : IRange<DateTime>
    {
        /// <summary>
        /// The beginning of the date range
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// The end of the date range
        /// </summary>
        public DateTime End { get; private set; }

        public DateRange(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public bool Includes(DateTime value)
        {
            // This expression determines if the value is in a given range.
            bool isInRange = ((Start <= value) && (value <= End));
            return isInRange;
        }

        public bool Includes(IRange<DateTime> range)
        {
            // This expression determines if the two ranges coincide.
            bool isInRange = ((Start <= range.Start) && (range.End <= End));
            return isInRange;
        }
    }
}
