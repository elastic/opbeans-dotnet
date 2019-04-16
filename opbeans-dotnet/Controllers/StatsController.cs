using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpbeansDotnet.Data;
using OpbeansDotnet.Model;

namespace OpbeansDotnet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatsController : ControllerBase
	{
		private readonly OpbeansDbContext _dbDbContext;

		public StatsController(OpbeansDbContext dbDbContext) => _dbDbContext = dbDbContext;

		public ActionResult<Stat> Get()
		{
			var retVal = new Stat();
			retVal.Orders = _dbDbContext.Orders.Count();
			retVal.Products = _dbDbContext.Orders.Count();
			retVal.Customers = _dbDbContext.Customers.Count();

			retVal.Numbers = new Numbers();
			retVal.Numbers.Revenue =
				_dbDbContext.OrderLines.Join(_dbDbContext.Products,
						orderLine => orderLine.OrderId,
						product => product.Id,
						(order, product) => new {Order = order, Product = product})
					.Sum(n => n.Order.Amount * n.Product.SellingPrice);

			retVal.Numbers.Cost =
				_dbDbContext.OrderLines.Join(_dbDbContext.Products,
						orderLine => orderLine.OrderId,
						product => product.Id,
						(order, product) => new {Order = order, Product = product})
					.Sum(n => n.Order.Amount * n.Product.Cost);

			retVal.Numbers.Profit =
				_dbDbContext.OrderLines.Join(_dbDbContext.Products,
						orderLine => orderLine.OrderId,
						product => product.Id,
						(order, product) => new {Order = order, Product = product})
					.Sum(n => n.Order.Amount * (n.Product.SellingPrice - n.Product.Cost));

			return retVal;
		}
	}
}
