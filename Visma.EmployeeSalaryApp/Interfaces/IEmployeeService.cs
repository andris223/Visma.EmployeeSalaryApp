using Visma.EmployeeSalaryApp.Models;

namespace Visma.EmployeeSalaryApp.Interfaces;

public interface IEmployeeService
{
    /// <summary>
    /// Shifts employee worked in specific month
    /// </summary>
    public ICollection<EmployeeShift> GetEmployeeShifts(int employeeId, int year, int month);
    
    /// <summary>
    /// Employee salary rate per hour in eur.
    /// </summary>
    public decimal GetEmployeeRate(int employeeId);
}