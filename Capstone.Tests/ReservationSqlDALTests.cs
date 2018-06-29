using System;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class ReservationSqlDALTests : DatabaseTests
	{
		[TestMethod]
		public void GetReservationInfo_Test()
		{
			ReservationSqlDAL dal = new ReservationSqlDAL(ConnectionString);

			Campground testCampground = new Campground();
			testCampground.CampgroundID = 1;
			Site testSite = new Site();
			testSite.SiteID = 1;

			dal.GetReservationInfo(testCampground);
			int rows = GetRowCount("reservation");

			Assert.AreEqual(1, rows);
		}
	}
}
