using AssetManager.Application.DTO.LoanAsset;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanAssetController : ControllerBase
    {
        private ILoanAssetRepository _loanAssetRepository;
        private IMapper _mapper;

        public LoanAssetController( IMapper mapper, ILoanAssetRepository loanAssetRepository)
        {
            _mapper = mapper;
            _loanAssetRepository = loanAssetRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Suporte")]
        public ActionResult<IEnumerable<LoanAssetEntity>> GetLoanAssetList(LoanAssetFilter filter)
        {
            var loanAssetList = _loanAssetRepository.LoanAssetList(filter);
            return Ok(loanAssetList);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public IActionResult ObterPorId(string id)
        {
            var loanAsset = _loanAssetRepository.GetByID(id);
            if(loanAsset != null)
                return Ok(loanAsset);

            return BadRequest("Não conseguimos localzar este ID em nosso sistema");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public Task<IActionResult> UpdateLoanAssetModel(string id, LoanAssetEntity laonAssetModel)
        {
            throw new Exception();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult CreateLoanAssetModel(CreateLoanAssetDTO loanAsset)
        {
            LoanAssetEntity loanAssetEntity = _mapper.Map<LoanAssetEntity>(loanAsset);
            var resultado = _loanAssetRepository.Create(loanAssetEntity);

            if (resultado.IsCompletedSuccessfully)
            {
                return Ok("Contrato registrado com sucesso");
            }
            return BadRequest("Houve um Erro");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult DeleteLoanAssetModel(string id)
        {
            var terminationLoanAsset = new TerminationLoanAssetViewModel();
            terminationLoanAsset.Date = DateTime.Now;
            terminationLoanAsset.Description = "Contrato de locação deletado!";
            
            if (EncerrandoContrato(id, terminationLoanAsset))
                return Ok("Termino de Contrato Registrado com sucesso!");
            else
                return BadRequest("Não foi possivel registrar o termino do contrato");
        }

        [HttpGet("ListarPorCompanhia/{idCompany}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult CompanyLoanAssetsList(int idCompany)
        {
            var loanList = _loanAssetRepository.CompanyLoanAssetsList(idCompany);
            
            if (loanList.IsCompleted && loanList.Result.Any())
                return Ok(loanList);

            return BadRequest();
        }
        
        [HttpGet("ListarPorUsuario/{idUsuario}")]
        [Authorize(Roles = "Administrador, Suporte, Funcionario")]
        public IActionResult UserAssetLoanList(string idUsuario)
        {
            var loanList = _loanAssetRepository.UserAssetLoanList(idUsuario, true);
            
            if (loanList.Any())
                return Ok(loanList);

            return BadRequest();
        }

        [HttpPut("EncerrandoContrato/{id}")]
        [Authorize(Roles = "Administrador, Suporte")]
        public IActionResult TerminationLoanAsset(string id, TerminationLoanAssetViewModel terminationLoanAsset)
        {
            try
            {
                if (EncerrandoContrato(id, terminationLoanAsset))
                    return Ok("Termino de Contrato Registrado com sucesso!");
                else
                    throw new Exception("Não foi possivel registrar o termino do contrato");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool EncerrandoContrato(string id, TerminationLoanAssetViewModel terminationLoanAsset)
        {
            var loanAsset = _loanAssetRepository.GetByID(id);
            loanAsset!.DevolutionDate = terminationLoanAsset.Date ?? DateTime.Now;
            loanAsset.Description = terminationLoanAsset.Description;
            return _loanAssetRepository.UpdateLocationAsset(loanAsset);
        }
    }
}
