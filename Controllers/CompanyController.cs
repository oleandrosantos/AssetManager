using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManager.Data;
using AssetManager.Model;
using Microsoft.AspNetCore.Authorization;
using AssetManager.Interfaces;
using AssetManager.ViewModel;
using AssetManager.Repository;
using AutoMapper;

namespace AssetManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private CompanyRepository _companyRepository;
        private IMapper _mapper;

        public CompanyController(CompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet("ListarCompanhias")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> CompanyList()
        {
            return await _companyRepository.CompanyList();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public async Task<ActionResult<CompanyModel>> GetCompanyByID(int id)
        {
            var companyModel = _companyRepository.GetCompanyByID(id);

            if (companyModel == null)
            {
                return NotFound();
            }

            return Ok(companyModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult PutCompanyModel(CompanyModel companyModel)
        {
            if (_companyRepository.UpdateCompany(companyModel).IsCompleted)
                return Ok("Atualizado com sucesso");

            return BadRequest("Não conseguimos Atualizar o companyModel");
        }

        [HttpPost]
        [Authorize(Roles = "Suporte")]
        public async Task<ActionResult<CompanyModel>> CreateCompany(CreateCompanyViewModel companyModel)
        {
            CompanyModel? companyResult = _companyRepository.CreateCompany(_mapper.Map<CreateCompanyViewModel, CompanyModel>(companyModel));

            if(companyResult != null)
               return Ok($"{companyResult.companyName} Cadastrada com sucesso!");
        
            return BadRequest($"Não conseguimos cadastrar, verifique os dados!");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Suporte")]
        public async Task<IActionResult> DeleteCompanyModel(int id)
        {
            if (_companyRepository.DeleteCompany(id).Result)
                return Accepted("Efetuada a exclusão da company");

            return BadRequest("A Companhia ja esta desativada");
        }

        public CompanyModel? ObterCompanyPorId(int id)
        {
            return _companyRepository.GetCompanyByID(id);
        }
    }
}
