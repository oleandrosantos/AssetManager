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

        [HttpPost("Create")]
        [Authorize(Roles = "Suporte")]
        public async Task<ActionResult<CompanyModel>> CreateCompany(CreateCompanyViewModel companyModel)
        {
            CompanyModel? companyResult = _companyRepository.CreateCompany(_mapper.Map<CreateCompanyViewModel, CompanyModel>(companyModel));

            if (companyResult != null)
                return Ok($"{companyResult.companyName} Cadastrada com sucesso!");

            return BadRequest($"Não conseguimos cadastrar, verifique os dados!");
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Suporte")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (_companyRepository.DeleteCompany(id))
                return Ok("Efetuada a exclusão da company");

            return BadRequest("A Companhia ja esta desativada");
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult UpdateCompany(int id, CompanyModel companyModel)
        {
            companyModel.idCompany = id;
            if (_companyRepository.UpdateCompany(companyModel))
                return Ok("Atualizado com sucesso");

            return BadRequest("Não conseguimos Atualizar o companyModel");
        }


        [HttpGet("ListarCompanhias")]
        [Authorize(Roles = "Suporte")]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> CompanyList()
        {
            return await _companyRepository.CompanyList();
        }

        [HttpGet("ObterCompany/{id}")]
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
    }
}
