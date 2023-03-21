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
  [ApiController]
  [Route("api/reporting-structure")]
  public class ReportingStructureController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly IReportingStructureService _reportingStructureService;

    public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
    {
      _logger = logger;
      _reportingStructureService = reportingStructureService;
    }

    [HttpGet("{employeeId}", Name = "getByEmployeeId")]
    public IActionResult GetByEmployeeId(String employeeId)
    {
      _logger.LogDebug($"Received reporting structure get request for employeeId '{employeeId}'");

      var reportingStructure = _reportingStructureService.GetByEmployeeId(employeeId);

      if (reportingStructure == null)
        return NotFound();

      return Ok(reportingStructure);
    }
  }
}
