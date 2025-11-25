using Api_iti.DTO;
using Api_iti.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_iti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ITI_Context _context;
        public DepartmentController(ITI_Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("Details")]
        public IActionResult getDeptDetails()
        {
            var deptList = _context.Departments.Include(d=>d.Emps).ToList();
            List<EmpDTO> deptListDto = new List<EmpDTO>();
            foreach(Department item in deptList)
            {
                EmpDTO dto = new EmpDTO();
                dto.Id = item.Id;
                dto.Name = item.Name;
                dto.EmpCount = item.Emps.Count;
                deptListDto.Add(dto);
            }

            return Ok(deptListDto);
        }

        [HttpGet]
        public IActionResult DisplayAllDept()
        {
            var departments = _context.Departments.ToList();
            return Ok(departments);
        }

        [HttpGet]
        [Route("{id:int}")] // api/Department/1
        public IActionResult DisplayDeptById(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return NotFound("Department Not Found");
            }
            return Ok(dept);
        }

        [HttpGet("{name:alpha}")]
        public IActionResult DisplayDeptByName(string name)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Name == name);
            if (dept == null)
            {
                return NotFound("Department Not Found");
            }
            return Ok(dept);
        }

        [HttpPost]
        public IActionResult AddNewDept(Department dept)
        {
            _context.Departments.Add(dept);
            _context.SaveChanges();
            //return Ok("Department Added Successfully");
            return CreatedAtAction("DisplayDeptById", new { id = dept.Id }, dept);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDept(int id, Department deptFromRequest)
        {
            var deptFromDB = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (deptFromDB == null)
            {
                return NotFound("Department Not Found");
            }
            deptFromDB.Name = deptFromRequest.Name;
            _context.SaveChanges();
            return Ok("Department Updated Successfully");
        }
    }
}
