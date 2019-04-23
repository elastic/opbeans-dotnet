using System.Collections.Generic;

namespace OpbeansDotnet.Model
{
	public class NewOrder
	{
		public int CustomerId { get; set; }

		public IEnumerable<OrderLine> Lines { get; set; }
	}
}
