using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
  public class Compensation
  {
    public string Id { get; set; }
    public string EmployeeId { get; set; }
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
    public int Salary { get; set; }
    public DateTime EffectiveDate { get; set; }
  }
}
