using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpbeansDotnet.Model
{
	public class OrderDetail
	{
		public long Id { get; set; }

		[JsonProperty("customer_id")] public long CustomerId { get; set; }

		[JsonProperty("created_at")] public string CreatedAt { get; set; }

		public Product Lines { get; set; }
	}
}
