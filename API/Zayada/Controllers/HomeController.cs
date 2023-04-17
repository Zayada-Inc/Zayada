using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ZayadaAPI.Controllers
{
    [Route("/")]
    [AllowAnonymous]
    public class HomeController : BaseApiController
    {
        [Cached(60)]
        [HttpGet]
        public ActionResult GetHome()
        {
            return Ok("Hello from Zayada API");
        }
    }
}
