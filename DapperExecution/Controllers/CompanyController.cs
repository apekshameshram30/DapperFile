using DapperExecution.DTO;
using DapperExecution.Sevices;
using Microsoft.AspNetCore.Mvc;

namespace DapperExecution.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var compani = await _companyRepository.GetAllCompanies();
                return Ok(compani);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet ("{id}", Name ="CompanyById")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            try
            {
                var compny = await _companyRepository.GetCompanyById(id);
                if(compny == null)
                {
                    return NotFound();
                }
                return Ok(compny);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> AddCompany(CompanyForCreationDto addCompny)
        //{
        //    var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country);" +
        //         " SELECT CAST(SCOPE_IDENTITY() as int)";
        //    int affectedRows = await _companyRepository.ExecuteAsync(query, addCompny);

        //    if (affectedRows > 0)
        //    {
        //        return Ok("User created successfully");
        //    }
        //    else
        //    {
        //        return BadRequest("Failed to create user");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyForCreationDto addCompny)
        {
            try
            {
                var addC = await _companyRepository.AddCompany(addCompny);
                return CreatedAtRoute("CompanyById", new { id = addC.CompanyId }, addC);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateCompany(CompanyForUpdateDto updateCompany, int id)
        {
            try
            {
                var update = await _companyRepository.GetCompanyById(id);
                if(update == null)
                {
                    return NotFound();
                }
                await _companyRepository.UpdateCompany(updateCompany,id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
 
        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var delete = await _companyRepository.GetCompanyById(id);
                if(delete == null)
                {
                    return NotFound();
                }
                await _companyRepository.DeleteCompany(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

  
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
