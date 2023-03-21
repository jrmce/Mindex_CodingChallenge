using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
  public interface ICompensationService
  {
    Compensation Create(Compensation comp);
    Compensation GetByEmployeeId(string employeeId);
  }
}
