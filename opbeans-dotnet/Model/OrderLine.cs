using System.ComponentModel.DataAnnotations.Schema;

namespace OpbeansDotnet.Model
{
	[Table("order_line")]
	public class OrderLine
	{
		public int Amount { get; set; }

		public int OderId { get; set; }

		public int ProductId { get; set; }
	}
}
