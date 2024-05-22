using Dapper;
using DapperExecution.Contex;
using DapperExecution.DTO;
using DapperExecution.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace DapperExecution.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly DapperContext _context;
        
        public EmployeeRepository (DapperContext context)
        {
            _context = context;
        }
        public async Task<Employees> AddEmployee(EmployeeDTO addEmployee)
        {
            var query = "INSERT INTO Employees(Name, Age, Position, CompanyId) VALUES (@Name, @Age, @Position, @CompanyId);"+
                        "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var connection = new DynamicParameters();
            connection.Add("Name", addEmployee.Name, DbType.String);
            connection.Add("Age", addEmployee.Age, DbType.Int32);
            connection.Add("Position", addEmployee.Position, DbType.String);
            connection.Add("CompanyId", addEmployee.CompanyId, DbType.Int32);

            using (var connect = _context.CreateConnection())
            {
                var id = await connect.QueryFirstOrDefaultAsync<int>(query, connection);
                var addedEmployee = new Employees
                {
                    Id = id,
                    Name = addEmployee.Name,
                    Age = addEmployee.Age,
                    Position = addEmployee.Position,
                    CompanyId = addEmployee.CompanyId,
                };
                return addedEmployee;
            }
        }
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployee()
        {
            var query = "Select * From Employees";
            using(var connection= _context.CreateConnection())
            {
                var get= await connection.QueryAsync<EmployeeDTO>(query);
                return get.ToList();
            }
        }
        
        //Get Employee using Stored Procedures
        //public async Task<Employees> GetEmployeeById(int id)
        //{
        //    using (var connection = new SqlConnection("connStr"))
        //    {
        //        var parameter = new DynamicParameters();
        //        parameter.Add("@Id", id, DbType.Int32);

        //        return await connection.QueryFirstOrDefaultAsync<Employees>("SpEmployee_Sel", parameter, commandType: CommandType.StoredProcedure);
        //    }
        //}

        public async Task <IEnumerable<Employees>> GetEmplyeesById(int id) //get By SP
        {
            using (var connection = new SqlConnection("connStr"))
            {
                var parameter = new { Id = id };
                var employees = await connection.QueryAsync<Employees>("SpEmployee_Sel", parameter, commandType: CommandType.StoredProcedure);
                return employees;
            }
        }
      
     //Get Employee WithOut Using Stored Procedure
       public async Task<Employees>  GetById(int id)
        {
            var query = "Select * From Employees Where Id=@Id";
            using (var connection = _context.CreateConnection())
            {
                var empl = await connection.QueryFirstOrDefaultAsync<Employees>(query, new {id });

                return empl; 
            }
        }

    }
}
