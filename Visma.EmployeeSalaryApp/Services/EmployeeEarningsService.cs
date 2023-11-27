using Visma.EmployeeSalaryApp.Exceptions;
using Visma.EmployeeSalaryApp.Interfaces;
using Visma.EmployeeSalaryApp.Models;


namespace Visma.EmployeeSalaryApp.Services
{
    public sealed class EmployeeEarningsService : IEmployeeEarningsService
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeEarningsService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public ICollection<EmployeeShift> CalculateShiftAmountEarned(int employeeId, int year, int month)
        {
            var shifts = _employeeService.GetEmployeeShifts(employeeId, year, month);
            var rate = _employeeService.GetEmployeeRate(employeeId);


            foreach (var shift in shifts)
            {
                shift.EarnedAmount = (double)rate * (shift.ShiftEnd - shift.ShiftStart).TotalHours;
            }
            return shifts;
        }

        public double CalculateMonthlyAmountEarned(int employeeId, int year, int month)
        {
            var salaryMonth = new DateTime(year: year, month: month, day: 1);

            if (DateTime.Now < salaryMonth)
            {
                throw new FutureDateTimeException(Constants.FutureDateProvided);
            }

            double amountEarned = 0;
            var shifts = _employeeService.GetEmployeeShifts(employeeId, year, month);
            var employeeRate = _employeeService.GetEmployeeRate(employeeId);

            foreach( var shift in shifts) { 
                var hoursWorked = shift.ShiftEnd - shift.ShiftStart;
                amountEarned += hoursWorked.TotalHours * (double)employeeRate;
            }
            
            return amountEarned;

        }
    }
}
