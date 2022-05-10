using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManager.Data;
using AssetManager.Model;
using Microsoft.AspNetCore.Authorization;
using AssetManager.Interfaces;
using AssetManager.ViewModel;

namespace AssetManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;
        private ICompanyService _companyService;

        public CompanyController(DataContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }



        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> Getcompany()
        {
            return await _context.company.ToListAsync();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Funcionario")]
        public async Task<ActionResult<CompanyModel>> GetCompanyModel(int id)
        {
            var companyModel = await _context.company.FindAsync(id);

            if (companyModel == null)
            {
                return NotFound();
            }

            return companyModel;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<IActionResult> PutCompanyModel(int id, CompanyModel companyModel)
        {
            if(_companyService.UpdateCompany(companyModel))
                return BadRequest("Não conseguimos Atualziar o companyModel");
            
            return Ok("Atualizado com sucesso");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CompanyModel>> CreateCompany(CreateCompanyViewModel companyModel)
        {
            var resultado = _companyService.CreateCompany(companyModel);
            if(resultado.IsCompleted && resultado.Result.status == true)
               return Ok(resultado.Result.mensagem);
        
            return BadRequest($"{resultado.Result.mensagem}");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteCompanyModel(int id)
        {
            if (_companyService.DeleteCompany(id).IsCompleted)
                return Accepted("Efetuada a exclusão da company");

            return BadRequest("A Companhia ja esta desativada");
        }

        private bool CompanyModelExists(int id)
        {
            return _context.company.Any(e => e.idCompany == id);
        }

        [HttpGet("ListarCompanhias")]
        [Authorize(Roles = "Administrador")]
        public List<CompanyModel> ListarCompany()
        {
            return _companyService.ListarCompany();
        }
    }
}
