using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpbeansDotnet.Data;
using OpbeansDotnet.Model;

namespace OpbeansDotnet.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly OpbeansDbContext _dbDbContext;

		public OrdersController(OpbeansDbContext dbDbContext) => _dbDbContext = dbDbContext;

		public ActionResult<IEnumerable<Order>> Get() => _dbDbContext.Orders.Select(n => Mapper.Map<Order>(n)).ToList();

		[HttpGet("{id:int?}")]
		public ActionResult<Order> Get(int id) =>
			_dbDbContext.Orders.Where(n => n.Id == id).Select(n => Mapper.Map<Order>(n)).First();

/**
 * Example body:
 * {
 *   customer_id: 1,
 *   lines: [
 *     {id: 1, amount: 1}
 *   ]
 * }
 */
		[HttpPost]
		public ActionResult Post([FromBody] NewOrder newOrder)
		{
			var insertedOrder = _dbDbContext.Orders.Add(new Orders
			{
				CreatedAt = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss.fff",
					CultureInfo.InvariantCulture),
				CustomerId = newOrder.CustomerId
			});

			foreach (var item in newOrder.Lines)
			{
				_dbDbContext.OrderLines.Add(new OrderLines
				{
					Amount = item.Amount,
					OrderId = insertedOrder.Entity.Id,
					ProductId = item.ProductId,
				});
			}

			_dbDbContext.SaveChanges();

			return Ok();
		}
	}
}
