using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ZayadaAPI.Controllers
{
    [Route("/")]
    [AllowAnonymous]
    public class HomeController : BaseApiController
    {

        [HttpGet]
        public ActionResult GetHome()
        {
            return Ok("Hello from Zayada API");
        }
    }
}
