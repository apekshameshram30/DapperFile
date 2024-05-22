using DapperExecution.DTO;
using DapperExecution.Model;

namespace DapperExecution.Repository
{
    public interface IEmployeeRepository
    {
        public Task<Employees> AddEmployee(EmployeeDTO addEmployee);

        public Task<IEnumerable<EmployeeDTO>> GetAllEmployee();

        // public Task<Employees> GetEmployeeById(int id);
        // public IQueryable<Employees> GetEmployeeById(int id);

        public Task<IEnumerable<Employees>> GetEmplyeesById(int id);

        public Task<Employees> GetById(int id);




    }
}
