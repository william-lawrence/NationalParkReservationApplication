using System;
using System.Collections.Generic;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationHandlerDALTests : DatabaseTests
    {
        [TestMethod]
        public void CreateReservation_Test()
        {
            Park testPark = new Park();
            DateTime testStartDate = DateTime.Now;
            DateTime testEndDate = testStartDate.AddDays(1);

            ReservationHandlerDAL dal = new ReservationHandlerDAL(testPark, 1, testStartDate, testEndDate, ConnectionString);

            int originalRows = GetRowCount("reservation");
            int testConfirmation = dal.CreateReservation(1, "test");
            int endRows = GetRowCount("reservation");

            Assert.AreEqual(originalRows + 1, endRows);
        }

        [TestMethod]
        public void CheckAvailability_Test()
        {
            Park testPark = new Park(1);
            DateTime testStartDate = DateTime.Now;
            DateTime testEndDate = testStartDate.AddDays(1);
            ReservationHandlerDAL dal = new ReservationHandlerDAL(testPark, 1, testStartDate, testEndDate, ConnectionString);

            List<Site> availableTestSites = new List<Site>(dal.CheckAvailabilty(testStartDate, testEndDate));

            Assert.AreEqual(0, availableTestSites.Count);
        }
    }
}
