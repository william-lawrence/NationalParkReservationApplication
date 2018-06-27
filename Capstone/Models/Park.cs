using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;

namespace Capstone.Models
{
	public class Park
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Location { get; set; }

		public DateTime EstDate { get; set; }

		public int Area { get; set; }

		public int Visitors { get; set; }

		public string Description { get; set; }

		public List<Campground> Campground { get; set; }

	}
}
