using System.ComponentModel.DataAnnotations.Schema;

namespace OpbeansDotnet.Data
{
	public class Products
	{
		public long Id { get; set; }
		public string Sku { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public long Stock { get; set; }
		public long Cost { get; set; }
		public long SellingPrice { get; set; }
		public long TypeId { get; set; }

		[ForeignKey("TypeId")] public ProductTypes Type { get; set; }
	}
}
