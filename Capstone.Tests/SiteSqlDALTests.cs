using System;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class SiteSqlDALTests : DatabaseTests
	{
		[TestMethod]
		public void GetSiteInfo_Test()
		{
			SiteSqlDAL dal = new SiteSqlDAL(ConnectionString);

			Park testPark = new Park(1);
			Campground testCampground = new Campground();
			testCampground.CampgroundID = 1;

			testPark.Campgrounds.Add(testCampground);

			dal.GetSiteInfo(testPark);

			Assert.AreEqual(1, testCampground.Sites.Count);
		}
	}
}
