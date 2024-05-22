using DapperExecution.DTO;
using DapperExecution.Model;

namespace DapperExecution.Sevices
{
    public interface ICompanyRepository
    {
     
        public  Task<IEnumerable<Companies>> GetAllCompanies();

        public  Task<Companies> GetCompanyById(int companyId);

        //public Task<Companies> GetCompanyById(int id);

        public Task<Companies> AddCompany(CompanyForCreationDto addCompny);

        public  Task UpdateCompany(CompanyForUpdateDto updateCompny, int id);

        public  Task DeleteCompany(int id);
    }
}
