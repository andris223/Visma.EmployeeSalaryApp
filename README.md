## Employee salary app

### Summary
Extend this simple web application allowing a manager to view shifts reported by an employee. The application consists of a .NET backend and an Angular front-end.

### Setup
* Pull project from github.
* Open the solution in Visual Studio or Rider to run the application.
* The Angular dev server will automatically host and open the main page.

### Existing Structure
The backend currently features an EmployeeService which must be used to get employee shifts and salary rate. Please do **not** modify EmployeeService, as it returns mock data necessary for the task.

The UI features a single page with 3 inputs: employee id, year, and month. The manager can view specific employee shifts for the selected month/year by altering the input values.

![image](https://github.com/AkimsP-Visma/Visma.EmployeeSalaryApp/assets/74917569/4ecf5320-455f-41f6-806c-f80730c6118e)

### Task
* 1\. Extend the application, by creating a **new** back-end service that uses data provided by IEmployeeService and:

  * 1.1\. Compute the amount earned by an employee for each shift.<br>
    (formula: <ins>shift length in hours</ins> * <ins>hourly salary rate</ins>, example: `7.5h * 10eur/h = 75eur`)

  * 1.2\. Compute the total amount earned by an employee in a month.

  * 1.3\. Validate that year/month are in the past (it's should **NOT** be possible to view salary data for current and future months). You may add other validations deemed necessary. Please implement validations inside the service itself, throw appropriate exception on validation failure.

  * 1.4\. **Write unit tests for your service**, using any preferred libraries (NUnit/XUnit, Moq/NSubstitute, FluentAssertions etc.)


* 2\. Create new endpoint in *employees controller* to return calculated values along with the shifts.

* 3\. Alter the main page (*salary-overview page*) to display the salary amount for each shift and the entire month.

### How to submit assignment
* Fork this repository.
* Commit all your changes.
* Share the repository link with us.
