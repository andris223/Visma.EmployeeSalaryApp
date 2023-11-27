using Microsoft.AspNetCore.Mvc;
using System;
using Visma.EmployeeSalaryApp.Interfaces;
using Visma.EmployeeSalaryApp.Models;

namespace Visma.EmployeeSalaryApp.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IEmployeeEarningsService _employeeEarningsService;

    public EmployeesController(IEmployeeService employeeService, IEmployeeEarningsService employeeEarningsService)
    {
        _employeeService = employeeService;
        _employeeEarningsService = employeeEarningsService;
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

    [HttpGet("{employeeId:int}/amount-earned-shift/{year:int}-{month:int}")]
    public ICollection<EmployeeShift> CalculateShiftAmountEarned([FromRoute] int employeeId, [FromRoute] int year, [FromRoute] int month)
    {
        return _employeeEarningsService.CalculateShiftAmountEarned(employeeId, year, month);
    }
    
    [HttpGet("{employeeId:int}/amount-earned-month/{year:int}-{month:int}")]
    public double CalculateMonthlyAmountEarned([FromRoute] int employeeId, [FromRoute] int year, [FromRoute] int month)
    {
        return _employeeEarningsService.CalculateMonthlyAmountEarned(employeeId, year, month);
    }
}