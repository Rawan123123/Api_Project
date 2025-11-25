using Api_iti.DTO;
using Api_iti.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_iti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITI_Context _context;
        public EmployeeController(ITI_Context context)
        {
            _context = context;
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var emp = _context.Employee.FirstOrDefault(e => e.Id == id);
            GeneralResponse generalResponse = new GeneralResponse();
            if(emp != null)
            {
                generalResponse.IsSuccess = true;
                generalResponse.Data = emp;
            }
            else
            {
                generalResponse.IsSuccess = false;
                generalResponse.Data = "Employee Not Found";
            }
                return Ok(generalResponse);
        }
       

    }
}
