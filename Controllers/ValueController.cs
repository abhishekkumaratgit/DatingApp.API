using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class ValueController : APIController
    {
        public IActionResult Get()
        {
            return Ok("Value");
        }
    }
}