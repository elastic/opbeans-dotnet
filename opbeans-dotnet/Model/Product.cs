using Newtonsoft.Json;

namespace OpbeansDotnet.Model
{
	public class Product
	{
		public long Id { get; set; }

		public string Sku { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public ProductType Type { get; set; }

		[JsonProperty("selling_price")] public decimal SellingPrice { get; set; }

		public int Stock { get; set; }
	}
}
