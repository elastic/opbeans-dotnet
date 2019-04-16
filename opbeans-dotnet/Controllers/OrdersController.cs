using System.Collections.Generic;
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
	}
}
