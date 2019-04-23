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
	public class TypesController : ControllerBase
	{
		private readonly OpbeansDbContext _dbDbContext;

		public TypesController(OpbeansDbContext dbDbContext)
			=> _dbDbContext = dbDbContext;

		public ActionResult<IEnumerable<ProductType>> Get() =>
			_dbDbContext.ProductTypes.Select(n => Mapper.Map<ProductType>(n)).ToList();

		[HttpGet("{id:int?}")]
		public ActionResult<ProductType> Get(int id) => _dbDbContext.ProductTypes.Where(n => n.Id == id)
			.Select(n => Mapper.Map<ProductType>(n)).First();
	}
}
