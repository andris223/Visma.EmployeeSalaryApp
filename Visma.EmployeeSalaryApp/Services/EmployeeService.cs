using Visma.EmployeeSalaryApp.Interfaces;
using Visma.EmployeeSalaryApp.Models;

namespace Visma.EmployeeSalaryApp.Services;

/// <summary>
/// Dot not modify this class, used only to mock employee data
/// </summary>
public sealed class EmployeeService : IEmployeeService
{
    /// <summary>
    /// Do not modify
    /// </summary>
    public ICollection<EmployeeShift> GetEmployeeShifts(int employeeId, int year, int month)
    {
        return GetEmployeeShiftsEnumerable(employeeId, year, month).ToList();
    }
    
    /// <summary>
    /// Do not modify
    /// </summary>
    public IEnumerable<EmployeeShift> GetEmployeeShiftsEnumerable(int employeeId, int year, int month)
    {
        var random = new Random(employeeId + year + month);

        var monthStart = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Local);
        var monthEnd = monthStart.AddDays(DateTime.DaysInMonth(year, month) - 1);
        for (var day = monthStart; day <= monthEnd; day = day.AddDays(1))
        {
            if (day.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                continue;
            }

            yield return new EmployeeShift()
            {
                ShiftStart = day.AddHours(8).AddMinutes(random.Next(120) - 60),
                ShiftEnd = day.AddHours(16).AddMinutes(random.Next(120) - 60)
            };
        }
    }

    /// <summary>
    /// Do not modify
    /// </summary>
    public decimal GetEmployeeRate(int employeeId)
    {
        var random = new Random(employeeId);
        return 7 + random.Next(12) / 2.0M;
    }
}