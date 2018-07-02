using System;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkSqlDALTests : DatabaseTests
    {
        [TestMethod]
        public void ListParks_Test()
        {
            ParkSqlDAL dal = new ParkSqlDAL(ConnectionString);

            var parks = dal.GetAllParks();
            int rows = GetRowCount("park");

            Assert.AreEqual(rows, parks.Count);
        }

        [TestMethod]
        public void GetParkInfo_Test()
        {
            ParkSqlDAL dal = new ParkSqlDAL(ConnectionString);
            Park testPark = new Park(1);

            dal.GetParkInfo(testPark);

            Assert.AreEqual(1, testPark.Id);
        }
    }
}
