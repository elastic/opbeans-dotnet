using Newtonsoft.Json;

namespace OpbeansDotnet.Model
{
	public class Customer
	{
		public long Id { get; set; }

		[JsonProperty("full_name")] public string FullName { get; set; }

		[JsonProperty("company_name")] public string CompanyName { get; set; }

		public string Email { get; set; }

		public string Address { get; set; }

		[JsonProperty("postal_code")] public string PostalCode { get; set; }

		public string City { get; set; }

		public string Country { get; set; }
	}
}
