using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class BaseApiController : Controller
	{
	}
}
