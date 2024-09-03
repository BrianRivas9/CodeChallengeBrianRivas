using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [Route("api/compensation")]
    [ApiController]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        //Method: CreateCompensation
        //Description: Creates a compensation object and stores it in an In-memory DB
        //Returns: Compensation Object that contains the EmployeeId, Salary and EffectiveDate
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

            _compensationService.CreateCompensation(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.EmployeeId }, compensation);
        }

        //Method: GetCompensationById
        //Description: Gets the Compensation Object stored based off the EmployeeId passed in
        //Returns: Compensation Object requested that contains the EmployeeId, Salary and EffectiveDate
        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var compensation = _compensationService.GetCompensation(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
