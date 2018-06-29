using System;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class CampgroundSqlDALTests : DatabaseTests
	{
		[TestMethod]
		public void GetCampgroundInfo_Test()
		{
			CampgroundSqlDAL dal = new CampgroundSqlDAL(ConnectionString);

			Park testPark = new Park(1);

			dal.GetCampgroundInfo(testPark);

			Assert.AreEqual(1, testPark.Campgrounds.Count);
		}
	}
}
