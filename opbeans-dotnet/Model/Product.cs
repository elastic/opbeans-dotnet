using Newtonsoft.Json;

namespace OpbeansDotnet.Model
{
	public class Product
	{
		public long Id { get; set; }

		public string Sku { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int Cost { get; set; }

		public string Type_id { get; set; }

		public string Type_name { get; set; }

		[JsonProperty("selling_price")] public int SellingPrice { get; set; }

		public int Stock { get; set; }
	}
}
