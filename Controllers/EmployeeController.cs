using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employeeSampleBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
   
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly EmployeeDbContext _EmployeeDbContext;

    public EmployeeController(ILogger<WeatherForecastController> logger,EmployeeDbContext EmployeeDbContext)
    {
        _logger = logger;
        _EmployeeDbContext = EmployeeDbContext;

    }

    [HttpGet("GetEmployee")]
    public async Task<IActionResult> GetEmployee()
    {
        var Employees = await _EmployeeDbContext.Employees.ToListAsync();
        return Ok(Employees);
    }

     [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody]Employee emp)
    {
        emp.Id = new Guid ();
         await _EmployeeDbContext.Employees.AddAsync(emp);
         await _EmployeeDbContext.SaveChangesAsync();
        return Ok(emp);
    }

     [HttpPut]
     [Route("{id:guid}")]
    public async Task<IActionResult> updateEmployee([FromRoute] Guid id, [FromBody]Employee emp)
    {
        var Employee = await _EmployeeDbContext.Employees.FirstOrDefaultAsync(s=>s.Id==id);
       if(Employee != null)
       {
        Employee.Name =emp.Name;
        Employee.EmailId =emp.EmailId;
        Employee.MobileNo= emp.MobileNo;
        await _EmployeeDbContext.SaveChangesAsync();
        return Ok(emp);
       }
       else{
        return NotFound("Employee Not Found");
       }
    }


    [HttpDelete]
     [Route("{id:guid}")]
    public async Task<IActionResult> deleteEmployee([FromRoute] Guid id)
    {
        var employee = await _EmployeeDbContext.Employees.FirstOrDefaultAsync(s=>s.Id==id);
       if(employee!=null)
       {
      _EmployeeDbContext.Remove(employee);
        await _EmployeeDbContext.SaveChangesAsync();
        return Ok(employee);
       }
       else{
        return NotFound("Employee Not Found");
       }
    }



  
}
