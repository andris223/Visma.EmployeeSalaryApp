using Visma.EmployeeSalaryApp.Models;

namespace Visma.EmployeeSalaryApp.Interfaces;

public interface IEmployeeEarningsService
{
    /// <summary>
    /// Amount Employee has earned in one shift
    /// </summary> 
    public ICollection<EmployeeShift> CalculateShiftAmountEarned(int employeeId, int year, int month);

    /// <summary>
    /// Amount Employee has earned in one month
    /// </summary>
    public double CalculateMonthlyAmountEarned(int employeeId, int year, int month);
}