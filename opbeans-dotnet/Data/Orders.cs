using System.ComponentModel.DataAnnotations.Schema;

namespace OpbeansDotnet.Data
{
	public class Orders
	{
		public long Id { get; set; }
		public string CreatedAt { get; set; }
		public long CustomerId { get; set; }

		[ForeignKey("CustomerId")]
		public Customers Customer { get; set; }
	}
}
