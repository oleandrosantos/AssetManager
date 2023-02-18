using AssetManager.Application.DTO.LoanAsset;
using AssetManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanAssetController : ControllerBase
    {
        private ILoanAssetService _loanAssetService;

        public LoanAssetController(ILoanAssetService loanAssetService)
        {
            _loanAssetService = loanAssetService;
        }

        [HttpGet("ObterPorId")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public IActionResult ObterPorId(string id)
        {
            var loanAsset = _loanAssetService.GetLoanByID(id);
            if (loanAsset != null)
                return Ok(loanAsset);

            return BadRequest("Não conseguimos localzar este ID em nosso sistema");
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Administrador,Suporte")]
        public Task<IActionResult> UpdateLoanAssetModel(LoanAssetDTO laonAssetModel)
        {
            throw new Exception();
        }

        [HttpPost("LoanAsset")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult LoanAsset(LoanAssetDTO loanAsset)
        {
            var resultado = _loanAssetService.LoanAsset(loanAsset);

            if (resultado.IsCompletedSuccessfully)
            {
                return Ok("Contrato registrado com sucesso");
            }
            return BadRequest("Houve um Erro");
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult Delete(TerminationLoanAssetModel terminationLoanAsset)
        {
            try
            {
                var resultado = _loanAssetService.DevolutionAsset(terminationLoanAsset);
                return Ok("Termino de Contrato Registrado com sucesso!");
            }
            catch (Exception e){

                return BadRequest("Não foi possivel registrar o termino do contrato");
            }
        }

        [HttpGet("ListarPorCompanhia/{idCompany}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult CompanyLoanAssetsList(int idCompany)
        {
            var loanList = _loanAssetService.GetLoanAssetsByCompany(idCompany);

            if (loanList.IsCompleted && loanList.Result.Any())
                return Ok(loanList);

            return BadRequest();
        }

        [HttpGet("ListarPorUsuario/{idUsuario}")]
        [Authorize(Roles = "Administrador, Suporte, Funcionario")]
        public IActionResult UserAssetLoanList(string idUsuario)
        {
            var loanList = _loanAssetService.GetLoanAssetsByUser(idUsuario);

            if (loanList.Result.Any())
                return Ok(loanList);

            return BadRequest();
        }

        [HttpPut("EncerrandoContrato")]
        [Authorize(Roles = "Administrador, Suporte")]
        public IActionResult TerminationLoanAsset(TerminationLoanAssetModel terminationLoanAsset)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
