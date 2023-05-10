using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace employeeSampleBackEnd;

public class EmployeeDbContext:DbContext
{

public  EmployeeDbContext(DbContextOptions options):base(options)
{
}

public DbSet<Employee>?  Employees { get; set; }

}
