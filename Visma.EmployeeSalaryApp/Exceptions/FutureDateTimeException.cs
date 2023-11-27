namespace Visma.EmployeeSalaryApp.Exceptions
{
    public class FutureDateTimeException : Exception
    {
        public FutureDateTimeException(string message) : base(message)
        {

        }
    }
}
