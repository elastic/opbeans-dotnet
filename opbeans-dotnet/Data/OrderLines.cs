namespace OpbeansDotnet.Data
{
	public class OrderLines
	{
		public long Id { get; set; }
		public long OrderId { get; set; }
		public long Amount { get; set; }
		public long ProductId { get; set; }
	}
}
