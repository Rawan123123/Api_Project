using Api_iti.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_iti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpGet("{name:alpha}/{age:int}")]
        public IActionResult testPrimitive(int age , string? name)
        {
            return Ok();
        }

        [HttpPost("{name}")]
        public IActionResult testObject(Department dept , string name)
        {
            return Ok();
        }
        [HttpGet("{Id}/{Name}/{ManagerName}")]
        public IActionResult testCustomBind([FromRoute] Department dept)
        {
            /*
             var Id = Request.RouteValues["Id"];
             var Name = Request.RouteValues["Name"];
             var ManagerName = Request.RouteValues["ManagerName"];
             */
            return Ok();
        }
    }
}
