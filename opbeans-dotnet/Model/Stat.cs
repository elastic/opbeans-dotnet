namespace OpbeansDotnet.Model
{
	public class Stat
	{
		public long Products { get; set; }

		public long Customers { get; set; }

		public long Orders { get; set; }

		public Numbers Numbers { get; set; }
	}

	public class Numbers
	{
		public double Revenue { get; set; }

		public double Cost { get; set; }

		public double Profit { get; set; }
	}
}
