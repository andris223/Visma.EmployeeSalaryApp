using Microsoft.AspNetCore.Mvc;
using Visma.EmployeeSalaryApp.Interfaces;
using Visma.EmployeeSalaryApp.Models;

namespace Visma.EmployeeSalaryApp.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("{employeeId:int}/shifts/{year:int}-{month:int}")]
    public ICollection<EmployeeShift> GetEmployeeShifts([FromRoute] int employeeId, [FromRoute] int year, [FromRoute] int month)
    {
        return _employeeService.GetEmployeeShifts(employeeId, year, month);
    }
    
    [HttpGet("{employeeId:int}/salary-rate")]
    public decimal GetEmployeeSalaryRate([FromRoute] int employeeId)
    {
        return _employeeService.GetEmployeeRate(employeeId);
    }
}