using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
	public class ParkSqlDAL
	{
		private string connectionString;

		public ParkSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		public IList<Park> GetAllParks()
		{
			IList<Park> output = new List<Park>();
		}
	}
}
