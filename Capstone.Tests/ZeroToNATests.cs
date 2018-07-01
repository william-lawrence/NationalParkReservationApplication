using System;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class ZeroToNATests
    {
        [TestMethod]
        public void ZeroToNATest_LengthIsZero()
        {
            Park park = new Park(1);
            BookingSubMenuCLI bookingSubMenuCLI = new BookingSubMenuCLI(park);

            int testValue = 0;

            Assert.AreEqual<string>("N/A", bookingSubMenuCLI.ZeroToNA(testValue));
        }

        [TestMethod]
        public void ZeroToNATest_LengthIsNOTZero()
        {
            Park park = new Park(1);
            BookingSubMenuCLI bookingSubMenuCLI = new BookingSubMenuCLI(park);

            int testValue = 25;

            Assert.AreEqual<string>("25", bookingSubMenuCLI.ZeroToNA(testValue));
        }
    }
}
