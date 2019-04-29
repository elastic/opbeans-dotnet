using Microsoft.AspNetCore.Mvc;

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
