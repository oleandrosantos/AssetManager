using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using AssetManager.Application.Interfaces;
using AssetManager.Application.DTO.Company;

namespace AssetManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _companyService;
        private IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public IActionResult CreateCompany(CreateCompanyDTO companyModel)
        {
            var companyResult = _companyService.CreateCompany(companyModel);

            if (companyResult != null)
                return Ok(companyResult);

            return BadRequest($"Não conseguimos cadastrar, verifique os dados!");
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Suporte")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            throw new NotImplementedException();
            //bool deletado = await _companyService.DeleteCompany(id);
            //if (deletado)
            //    return Ok("Efetuada a exclusão da company");

            //return BadRequest("A Companhia ja esta desativada");
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult UpdateCompany(int id, CompanyDTO companyModel)
        {
            throw new NotImplementedException();

            //companyModel.IdCompany = id;
            //if (_companyService.UpdateCompany(companyModel))
            //    return Ok("Atualizado com sucesso");

            //return BadRequest("Não conseguimos Atualizar o companyModel");
        }


        [HttpGet("ListarCompanhias")]
        [Authorize(Roles = "Suporte")]
        public IActionResult CompanyList()
        {
            throw new NotImplementedException();

            //List<CompanyModel> companyList = _companyService.CompanyList().Result;

            //if (companyList.Any() || companyList != null)
            //    return Ok(companyList);
            //else if (companyList.Any())
            //    return NoContent();

            //return BadRequest();            
        }

        [HttpGet("ObterCompany/{id}")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public IActionResult GetCompanyByID(int id)
        {
            throw new NotImplementedException();

            //var companyModel = _companyService.GetCompanyByID(id);

            //if (companyModel == null)
            //{
            //    return NotFound();
            //}

            //return Ok(companyModel);
        }
    }
}
