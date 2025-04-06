using EmployeeManagementGrapghQLReact.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementGrapghQLReact.Server.Repositories;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext()
    {
    }
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=EmployeeGrapghQLReactDb;User ID=sms;Password=Test@123;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }
    }
}
