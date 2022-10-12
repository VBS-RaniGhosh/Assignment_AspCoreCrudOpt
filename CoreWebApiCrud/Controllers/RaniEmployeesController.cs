using CoreWebApiCrud.Repositories;
using DataAccessLayerOne;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaniEmployeesController : ControllerBase
    {
        private readonly IRaniEmployeeRepository _raniEmployeeRepository;

        public RaniEmployeesController(IRaniEmployeeRepository raniEmployeeRepository)
        {
            _raniEmployeeRepository = raniEmployeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetRaniEmployees()
        {
            try
            {
                return Ok(await _raniEmployeeRepository.GetRaniEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RaniEmployee>> GetRaniEmployee(int id)
        {
            try
            {
                var result = await _raniEmployeeRepository.GetRaniEmployee(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");
            }

        }

        [HttpPost]
        public async Task<ActionResult<RaniEmployee>> CreateRaniEmployee(RaniEmployee raniEmployee)
        {

            try
            {
                if (raniEmployee == null)
                {
                    return BadRequest();
                }
                var CreatedRaniEmployee = await _raniEmployeeRepository.AddRaniEmployee(raniEmployee);
                return CreatedAtAction(nameof(GetRaniEmployee), new { id = CreatedRaniEmployee.Id }, CreatedRaniEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RaniEmployee>> UpdateRaniEmployee(int id, RaniEmployee raniEmployee)
        {
            try
            {

                if (id != raniEmployee.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var raniEmployeeUpdate = await _raniEmployeeRepository.GetRaniEmployee(id);
                if (raniEmployeeUpdate == null)
                {
                    return NotFound($"Employee Id={id} not Found");
                }
                return await _raniEmployeeRepository.UpdateRaniEmployee(raniEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");
            }
        }

        [HttpDelete("{id:int}")] /*https://localhost:5001/api/raniemployees/Search?name=rani*/
        public async Task<ActionResult<RaniEmployee>> DeleteRaniEmployee(int id)
        {
            try
            {

                var raniEmployeeDelete = await _raniEmployeeRepository.GetRaniEmployee(id);
                if (raniEmployeeDelete == null)
                {
                    return NotFound($"Employee Id={id} not Found");
                }
                return await _raniEmployeeRepository.DeleteRaniEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data from Database");
            }

        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<RaniEmployee>>> SearchRaniEmployee(string name)
        {
            try
            {
                var result = await _raniEmployeeRepository.SearchRaniEmployee(name);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error in Retrieving Data from Database");
            }
        }

    }
}
