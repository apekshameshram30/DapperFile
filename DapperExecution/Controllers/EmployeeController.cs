using DapperExecution.DTO;
using DapperExecution.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace DapperExecution.Controllers
{
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepository = employeeRepo;
        }

        [HttpPost ("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDTO addEmployee)
        {
            var addEmp = await _employeeRepository.AddEmployee(addEmployee);
            // return Ok(addEmp);
            return CreatedAtRoute("EmployeeById", new { id = addEmp.Id }, addEmp); 
        }

        [HttpGet ("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var getEmp = await _employeeRepository.GetAllEmployee();
            return Ok(getEmp);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var getById = await _employeeRepository.GetById(id);
            if (getById == null)
            {
                return NotFound();
            }
            return Ok(getById);
        }


        [HttpGet("GetEmployeeById")]
        public IActionResult GetEmployeeById(int id)
        {
            var getById = _employeeRepository.GetEmplyeesById(id);
            if (getById == null)
            {
                return NotFound();
            }
            return Ok(getById);
        }



    }
}
