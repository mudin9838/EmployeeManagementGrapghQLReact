using EmployeeManagementGrapghQLReact.Server.Data;
using EmployeeManagementGrapghQLReact.Server.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementGrapghQLReact.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly IValidator<Employee> _employeeValidator; // inject the validator>

    public EmployeeController(EmployeeRepository employeeRepository, IValidator<Employee> employeeValidator)
    {
        _employeeRepository = employeeRepository;
        _employeeValidator = employeeValidator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }


    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
    {
        if (employee == null)
        {
            return BadRequest("Employee data is required.");
        }

        //validate the employee using fluent validation
        var result = await _employeeValidator.ValidateAsync(employee);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _employeeRepository.CreateEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
    {
        if (employee == null || id != employee.Id)
        {
            return BadRequest("Employee data is invalid");
        }
        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }
        //validate
        var validationResult = await _employeeValidator.ValidateAsync(employee);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        await _employeeRepository.UpdateEmployeeAsync(employee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        await _employeeRepository.DeleteEmployeeAsync(id);
        return NoContent();
    }
}
