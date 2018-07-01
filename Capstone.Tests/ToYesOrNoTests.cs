using System;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass, TestCategory("BookingMenuCLI")]
    public class ToYesOrNoTests
    {
        [TestMethod]
        public void ToYesOrNo()
        {
            Park park = new Park(1);
            BookingSubMenuCLI bookingSubMenuCLI = new BookingSubMenuCLI(park);

            bool test1Value = true;
            bool test2Value = false;

            Assert.AreEqual<string>("Yes", bookingSubMenuCLI.ToYesOrNo(test1Value));
            Assert.AreEqual<string>("No", bookingSubMenuCLI.ToYesOrNo(test2Value));
        }
    }
}
