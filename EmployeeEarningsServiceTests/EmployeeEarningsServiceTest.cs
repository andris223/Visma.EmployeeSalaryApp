using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Moq;
using System.Text.RegularExpressions;
using Visma.EmployeeSalaryApp.Interfaces;
using Visma.EmployeeSalaryApp.Models;
using Visma.EmployeeSalaryApp.Services;

namespace EmployeeEarningsServiceTests
{
    public class EmployeeEarningsServiceTest
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;

        public EmployeeEarningsServiceTest() 
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
        }


        [Fact]
        public void CalculateShiftAmountEarned_NoShiftsGiven_ReturnZero()
        {
            _mockEmployeeService.Setup(x => x.GetEmployeeShifts(1, 2005, 2)).Returns(new List<EmployeeShift>());
            _mockEmployeeService.Setup(x => x.GetEmployeeRate(It.IsAny<int>())).Returns(8.5M);

            var employeeEarningsService = new EmployeeEarningsService(_mockEmployeeService.Object);

            var actual = employeeEarningsService.CalculateShiftAmountEarned(1, 2005, 2);

            _mockEmployeeService.Verify(x => x.GetEmployeeShifts(1, 2005, 2), Times.Once);
            _mockEmployeeService.Verify(x => x.GetEmployeeRate(It.IsAny<int>()), Times.Once);
            actual.Should().HaveCount(0);
        }

        [Fact]
        public void CalculateShiftAmountEarned_GivenShifts_ReturnExpectedAmount()
        {
            var expectedList = new List<EmployeeShift>();
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-01T08:38:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-01T15:40:00+03:00"), EarnedAmount = 56.266666666666666 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-04T08:37:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-04T15:58:00+03:00"), EarnedAmount = 58.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-05T07:09:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-05T15:23:00+03:00"), EarnedAmount = 65.86666666666666 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-06T08:36:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-06T15:43:00+03:00"), EarnedAmount = 56.93333333333333 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-07T07:59:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-07T16:53:00+03:00"), EarnedAmount = 71.2 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-08T07:07:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-08T16:19:00+03:00"), EarnedAmount = 73.6 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-11T07:26:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-11T15:17:00+03:00"), EarnedAmount = 62.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-12T07:02:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-12T16:21:00+03:00"), EarnedAmount = 74.53333333333333 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-13T07:54:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-13T15:54:00+03:00"), EarnedAmount = 64 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-14T08:25:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-14T16:00:00+03:00"), EarnedAmount = 60.666666666666664 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-15T08:53:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-15T16:14:00+03:00"), EarnedAmount = 58.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-18T08:09:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-18T15:30:00+03:00"), EarnedAmount = 58.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-19T08:53:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-19T15:14:00+03:00"), EarnedAmount = 50.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-20T08:40:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-20T15:42:00+03:00"), EarnedAmount = 56.266666666666666 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-21T07:10:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-21T15:45:00+03:00"), EarnedAmount = 68.66666666666667 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-22T08:27:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-22T16:55:00+03:00"), EarnedAmount = 67.73333333333333 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-25T08:37:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-25T16:54:00+03:00"), EarnedAmount = 66.26666666666667 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-26T08:02:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-26T16:44:00+03:00"), EarnedAmount = 69.6 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-27T07:07:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-27T16:44:00+03:00"), EarnedAmount = 76.9333333333334 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-28T07:59:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-28T15:36:00+03:00"), EarnedAmount = 60.93333333333333 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-29T07:27:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-29T16:03:00+03:00"), EarnedAmount = 68.8 });

            _mockEmployeeService.Setup(x => x.GetEmployeeShifts(1, 2023, 9)).Returns(expectedList);
            _mockEmployeeService.Setup(x => x.GetEmployeeRate(1)).Returns(56.266666666666666M);

            var employeeEarningsService = new EmployeeEarningsService(_mockEmployeeService.Object);

            var actual = employeeEarningsService.CalculateShiftAmountEarned(1, 2023, 9);

            _mockEmployeeService.Verify(x => x.GetEmployeeShifts(1, 2023, 9), Times.Once);
            _mockEmployeeService.Verify(x => x.GetEmployeeRate(It.IsAny<int>()), Times.Once);
            actual.Should().HaveCount(21);
        }

        [Fact]
        public void CalculateMonthlyAmountEarned_NoShiftsGiven_ReturnZero()
        {
            _mockEmployeeService.Setup(x => x.GetEmployeeShifts(1, 2005, 2)).Returns(new List<EmployeeShift>());
            _mockEmployeeService.Setup(x => x.GetEmployeeRate(1)).Returns(8.5M);

            var employeeEarningsService = new EmployeeEarningsService(_mockEmployeeService.Object);

            var actual = employeeEarningsService.CalculateMonthlyAmountEarned(1, 2005, 2);

            _mockEmployeeService.Verify(x => x.GetEmployeeShifts(1, 2005, 2), Times.Once);
            _mockEmployeeService.Verify(x => x.GetEmployeeRate(1), Times.Once);
            actual.Should().Be(0);
        }

        [Fact]
        public void CalculateMonthlyAmountEarned_ShiftsGiven_ReturnExpectedAmount()
        {
            var expectedList = new List<EmployeeShift>();
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-01T08:38:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-01T15:40:00+03:00"), EarnedAmount = 56.266666666666666 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-04T08:37:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-04T15:58:00+03:00"), EarnedAmount = 58.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-05T07:09:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-05T15:23:00+03:00"), EarnedAmount = 65.86666666666666 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-06T08:36:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-06T15:43:00+03:00"), EarnedAmount = 56.93333333333333 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-07T07:59:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-07T16:53:00+03:00"), EarnedAmount = 71.2 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-08T07:07:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-08T16:19:00+03:00"), EarnedAmount = 73.6 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-11T07:26:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-11T15:17:00+03:00"), EarnedAmount = 62.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-12T07:02:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-12T16:21:00+03:00"), EarnedAmount = 74.53333333333333 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-13T07:54:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-13T15:54:00+03:00"), EarnedAmount = 64 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-14T08:25:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-14T16:00:00+03:00"), EarnedAmount = 60.666666666666664 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-15T08:53:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-15T16:14:00+03:00"), EarnedAmount = 58.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-18T08:09:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-18T15:30:00+03:00"), EarnedAmount = 58.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-19T08:53:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-19T15:14:00+03:00"), EarnedAmount = 50.8 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-20T08:40:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-20T15:42:00+03:00"), EarnedAmount = 56.266666666666666 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-21T07:10:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-21T15:45:00+03:00"), EarnedAmount = 68.66666666666667 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-22T08:27:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-22T16:55:00+03:00"), EarnedAmount = 67.73333333333333 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-25T08:37:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-25T16:54:00+03:00"), EarnedAmount = 66.26666666666667 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-26T08:02:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-26T16:44:00+03:00"), EarnedAmount = 69.6 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-27T07:07:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-27T16:44:00+03:00"), EarnedAmount = 76.9333333333334 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-28T07:59:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-28T15:36:00+03:00"), EarnedAmount = 60.93333333333333 });
            expectedList.Add(new EmployeeShift { ShiftStart = DateTime.Parse("2023-09-29T07:27:00+03:00"), ShiftEnd = DateTime.Parse("2023-09-29T16:03:00+03:00"), EarnedAmount = 68.8 });

            _mockEmployeeService.Setup(x => x.GetEmployeeShifts(1, 2023, 9)).Returns(expectedList);
            _mockEmployeeService.Setup(x => x.GetEmployeeRate(1)).Returns(8.5M);

            var employeeEarningsService = new EmployeeEarningsService(_mockEmployeeService.Object);

            var actual = employeeEarningsService.CalculateMonthlyAmountEarned(1, 2023, 9);

            _mockEmployeeService.Verify(x => x.GetEmployeeShifts(1, 2023, 9), Times.Once);
            _mockEmployeeService.Verify(x => x.GetEmployeeRate(1), Times.Once);
            actual.Should().BeApproximately(1441, 100);
        }
    }
}
