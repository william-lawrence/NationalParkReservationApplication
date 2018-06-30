using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    /// <summary>
    /// Object that represents the range of days between two days. 
    /// </summary>
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

        /// <summary>
        /// Constructor for the date range
        /// </summary>
        /// <param name="start">The beginning of the date range.</param>
        /// <param name="end">The end of the date range.</param>
        public DateRange(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Determines if a single date coincides with the date range.
        /// </summary>
        /// <param name="value">The date to check if is in the date range.</param>
        /// <returns></returns>
        public bool Includes(DateTime value)
        {
            // This expression determines if the value is in a given range.
            bool isInRange = ((Start <= value) && (value <= End));
            return isInRange;
        }

        /// <summary>
        /// Determines if a date range coincides with a date range.
        /// </summary>
        /// <param name="range">The date range to check if it coincide.</param>
        /// <returns></returns>
        public bool Includes(IRange<DateTime> range)
        {
            // This expression determines if the two ranges coincide.
            bool isInRange = ((Start <= range.Start) && (range.End <= End));
            return isInRange;
        }
    }
}
