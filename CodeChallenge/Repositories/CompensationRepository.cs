using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
  public class CompensationRepository : ICompensationRepository
  {
    private readonly EmployeeContext _employeeContext;
    private readonly ILogger<ICompensationRepository> _logger;

    public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
    {
      _employeeContext = employeeContext;
      _logger = logger;
    }

    public Compensation Add(Compensation comp)
    {
      _employeeContext.Compensations.Add(comp);
      return comp;
    }

    public Compensation GetByEmployeeId(string id)
    {
      return _employeeContext.Compensations.Include(comp => comp.Employee).SingleOrDefault(c => c.Employee.EmployeeId == id);
    }

    public Task SaveAsync()
    {
      return _employeeContext.SaveChangesAsync();
    }
  }
}
