using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpbeansDotnet.Data;
using OpbeansDotnet.Model;

namespace OpbeansDotnet.Controllers
{
	[Route("/")]
	[ApiController]
	public class RootController : ControllerBase
	{

		[HttpGet()]
		public ActionResult<string> Get() => "ok";
	}
}
