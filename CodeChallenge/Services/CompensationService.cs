using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
  public class CompensationService : ICompensationService
  {
    private readonly ICompensationRepository _compensationRepository;
    private readonly ILogger<CompensationService> _logger;

    public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
    {
      _compensationRepository = compensationRepository;
      _logger = logger;
    }

    public Compensation Create(Compensation comp)
    {
      if (comp != null)
      {
        _compensationRepository.Add(comp);
        _compensationRepository.SaveAsync().Wait();
      }

      return comp;
    }

    public Compensation GetByEmployeeId(string id)
    {
      if (!String.IsNullOrEmpty(id))
      {
        return _compensationRepository.GetByEmployeeId(id);
      }

      return null;
    }
  }
}
