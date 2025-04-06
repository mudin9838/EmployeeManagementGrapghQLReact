using EmployeeManagementGrapghQLReact.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementGrapghQLReact.Server.Repositories;

public class EmployeeRepository
{
    private readonly EmployeeDbContext _context;
    public EmployeeRepository(EmployeeDbContext context)
    {
        _context = context;
    }
    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }
    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }
    public async Task CreateEmployeeAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await GetEmployeeByIdAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
