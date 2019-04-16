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
	public class CustomersController : ControllerBase
	{
		private readonly OpbeansDbContext _dbDbContext;

		public CustomersController(OpbeansDbContext dbDbContext)
			=> _dbDbContext = dbDbContext;

		public ActionResult<IEnumerable<Customer>> Get() =>
			_dbDbContext.Customers.Select(n => Mapper.Map<Customer>(n)).ToList();

		[HttpGet("{id:int?}")]
		public ActionResult<Customer> Get(int id) => _dbDbContext.Customers.Where(n => n.Id == id)
			.Select(n => Mapper.Map<Customer>(n)).First();
	}
}
