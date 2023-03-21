using CodeChallenge.Models;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
  public class ReportingStructureService : IReportingStructureService
  {
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<ReportingStructureService> _logger;

    public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeService employeeService)
    {
      _employeeService = employeeService;
      _logger = logger;
    }

    public ReportingStructure GetByEmployeeId(string employeeId)
    {
      var employee = _employeeService.GetById(employeeId);
      var directReports = 0;
      GetDirectReports(employee, ref directReports);

      return new ReportingStructure
      {
        Employee = employee,
        NumberOfReports = directReports
      };
    }

    private static void GetDirectReports(Employee employee, ref int count)
    {
      if (employee.DirectReports == null)
      {
        return;
      }

      foreach (var directReport in employee.DirectReports)
      {
        count++;
        GetDirectReports(directReport, ref count);
      }
    }
  }
}
