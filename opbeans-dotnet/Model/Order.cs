using System;

namespace OpbeansDotnet.Model
{
	public class Order
	{
		public long ID { get; set; }

		public Customer Customer { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
