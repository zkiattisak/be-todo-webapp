using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestASPTodo.Data;
using TestASPTodo.Models;
using TestASPTodo.Models.Entities;
using TestASPTodo.Repository;

namespace TestASPTodo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly EmployeeRepository _employeeRepository;

        public EmployeesController(ApplicationDbContext dbContext , EmployeeRepository employeeRepository )
        { 
            this.dbContext = dbContext;
            _employeeRepository = employeeRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeesById(Guid id) 
        {
           var employee = dbContext.Employees.Find(id);
            if (employee == null) 
            {
                return NotFound();
            }
            return Ok(employee);

        }


        [HttpPost]
        public IActionResult AddEmployees(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Salary = addEmployeeDto.Salary,
            };
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id , UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null) { return NotFound(); };

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;   
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployees(Guid id)
        {
            var employee = dbContext.Employees.Find(id); if (employee == null) { return NotFound(); }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok(employee);

        }
    }
}
