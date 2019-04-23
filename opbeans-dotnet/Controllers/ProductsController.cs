using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpbeansDotnet.Data;
using OpbeansDotnet.Model;

namespace OpbeansDotnet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly OpbeansDbContext _dbDbContext;

		public ProductsController(OpbeansDbContext dbDbContext) => _dbDbContext = dbDbContext;

		public ActionResult<IEnumerable<Product>> Get() =>
			_dbDbContext.Products.Select(n => Mapper.Map<Product>(n)).ToList();

		[HttpGet("top")]
		public ActionResult<IEnumerable<Product>> Top() =>
			_dbDbContext.Products.FromSql(@"SELECT
				products.id, sku, name, description, cost, selling_price, stock, type_id , SUM(order_lines.amount) AS sold
				FROM products JOIN order_lines ON products.id=product_id GROUP BY products.id ORDER BY sold DESC")
				.Select(n => Mapper.Map<Product>(n)).ToList();

		[HttpGet("{id:int?}")]
		public ActionResult<Product> Get(int id) =>
			_dbDbContext.Products.Include(n => n.Type).Where(n => n.Id == id).Select(n => Mapper.Map<Product>(n))
				.First();

		[HttpGet("{id:int?}/customers")]
		public ActionResult<IEnumerable<Customer>> Customerwhobought(int id)
			=> _dbDbContext.OrderLines.Join(_dbDbContext.Customers,
					orderLine => orderLine.Id,
					customer => customer.Id,
					(orderLines, customer) => customer)
				.Select(n => Mapper.Map<Customer>(n)).ToList();
	}
}
