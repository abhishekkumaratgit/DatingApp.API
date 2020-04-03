using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class ValueController : APIController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Value");
        }
    }
}