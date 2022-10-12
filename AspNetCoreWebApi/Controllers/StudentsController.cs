using AspNetCoreWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        List<Student> _oStudents = new List<Student>()
        {
            new Student() {Id = 1, Name = "Rani" , Roll = 307},
            new Student() {Id = 2, Name = "Anjali" , Roll = 308},
            new Student() {Id = 3, Name = "Nisha" , Roll = 309},


        };

        [HttpGet]
        public IActionResult Gets()
        {
            if (_oStudents.Count == 0)
            {
                return NotFound("No list found");

            }
            return Ok(_oStudents);
        }
        [HttpGet("GetStudent")]

        public IActionResult Get(int id)
        {
            var oStudent = _oStudents.SingleOrDefault(x => x.Id == id);
            if(oStudent == null)
            {
                return NotFound("No student found.");
            }
            return Ok(oStudent);
        }
        [HttpPost]
        public IActionResult Save(Student oStudent)
        {
            _oStudents.Add(oStudent);
            if(_oStudents.Count == 0)
            {
                return NotFound("No List Found.");
            }
            return Ok(_oStudents);
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var oStudent = _oStudents.SingleOrDefault(x => x.Id == id);
            if (oStudent == null)
            {
                return NotFound("No student found ");

            }
            _oStudents.Remove(oStudent);
            
            if (_oStudents.Count == 0)
            {
                return NotFound("No list Found.");
            }
            return Ok(_oStudents);
        }
    }
}

