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
        public IActionResult Create(CreateCompanyDTO companyModel)
        {
            var companyResult = _companyService.Create(companyModel);

            if (companyResult.IsCompletedSuccessfully)
                return Ok("Companhia cadastrada com sucesso!");

            return BadRequest($"Não conseguimos cadastrar, verifique os dados!");
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Suporte")]
        public IActionResult Delete(int id)
        {
            try
            {
                _companyService.Delete(id);
                return Ok("Companhia Excluida com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel Excluir a companhia");
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult Update(int id, CompanyDTO companyModel)
        {
            try
            {
                if (id != companyModel.IdCompany)
                    throw new Exception();
                
                _companyService.Update(companyModel);

                return Ok("Usuario atualizado com sucesso!s");
            }
            catch(Exception ex)
            {
                return BadRequest("Houve um erro e não foi possivel atualizar a companhia");
            }
        }


        [HttpGet("ListarCompanhias")]
        [Authorize(Roles = "Suporte")]
        public IActionResult CompanyList()
        {
            try
            {
                var company = _companyService.GetAll().Result;
                return Ok(company);
            }
            catch (Exception e)
            {
                return NoContent();
            }

        }

        [HttpGet("ObterCompany/{id}")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public IActionResult GetByID(int id)
        {
            try
            {
                var company = _companyService.GetByID(id);
                return Ok(company);
            }
            catch(Exception e)
            {
                return NoContent();
            }
        }
    }
}
