using Dapper;
using DapperExecution.Contex;
using DapperExecution.DTO;
using DapperExecution.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DapperExecution.Sevices
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }
        //code for executing strored procedure using dapper
        public async Task<Companies> GetCompanyById(int companyId)
        {
            using (var connection = new SqlConnection("connStr"))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CompanyId", companyId, DbType.Int32);

                return await connection.QuerySingleOrDefaultAsync<Companies>("Exec SpDapper_Sel", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        ////end
        public async Task<IEnumerable<Companies>> GetAllCompanies()
        {
            string query = "Select * from Companies";
            using  (var connection= _context.CreateConnection())
            {
                var comp = await connection.QueryAsync<Companies>(query);
                return comp.ToList();
            }
        }

        //public async Task<Companies> GetCompanyById(int id)
        //{
        //    var query = "Select * From Companies Where  CompanyId= @Id";
        //    using (var connection = _context.CreateConnection())
        //    {
        //        var comp = await connection.QuerySingleOrDefaultAsync<Companies>(query, new { id });
        //        return comp;
        //    }
        //}
        public async Task<Companies> AddCompany(CompanyForCreationDto addCompny)
        {
            var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country);" +
                " SELECT CAST(SCOPE_IDENTITY() as int)";
            var connection = new DynamicParameters();
            connection.Add("Name", addCompny.Name, DbType.String);
            connection.Add("Address", addCompny.Address, DbType.String);
            connection.Add("Country", addCompny.Country, DbType.String);
            using (var connect = _context.CreateConnection())
            {
                var id = await connect.QuerySingleAsync<int>(query, connection);
                var createdCompany = new Companies
                {
                    CompanyId = id,
                    Name = addCompny.Name,
                    Address = addCompny.Address,
                    Country = addCompny.Country
                };
                return createdCompany;
            }
        }

        public async Task UpdateCompany(CompanyForUpdateDto updateCompny, int id)
        {
            var query = "Update Companies SET Name=@Name, Address=@Address, Country=@Country Where  CompanyId= @Id";
            var parameters= new DynamicParameters();
            parameters.Add("Id",id, DbType.Int32);
            parameters.Add("Name",updateCompny.Name, DbType.String);
            parameters.Add("Address",updateCompny.Address,DbType.String);
            parameters.Add("Country",updateCompny.Country,DbType.String);
            using(var connect=_context.CreateConnection())
            {
                await connect.ExecuteAsync( query, parameters);
            }

        }

        //public async Task DeleteCompany(int id)
        //{
        //    var query = "DELETE FROM Companies Where CompanyId = @Id";
        //    using (var connect = _context.CreateConnection())
        //    {
        //        await connect.ExecuteAsync(query, new { id });
        //    }
        //}

        public async Task DeleteCompany(int id)
        {
            var deleteEmployeesQuery = "DELETE FROM Employees WHERE CompanyId = @Id";
            var deleteCompanyQuery = "DELETE FROM Companies WHERE CompanyId = @Id";

            using (var connect = _context.CreateConnection())
            {
                await connect.ExecuteAsync(deleteEmployeesQuery, new { id });
                await connect.ExecuteAsync(deleteCompanyQuery, new { id });
            }
        }
    }
}
