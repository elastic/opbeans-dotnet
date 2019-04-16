using System.ComponentModel.DataAnnotations.Schema;

namespace OpbeansDotnet.Model
{
	[Table("order_line")]
	public class OrderLine
	{
		public int Amount { get; set; }

		[ForeignKey("order_id")] public Order Oder { get; set; }

		[ForeignKey("product_id")] public Product Product { get; set; }
	}
}
