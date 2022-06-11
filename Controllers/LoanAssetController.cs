#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;
using AssetManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AssetManager.Repository;
using AutoMapper;

namespace AssetManager
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanAssetController : ControllerBase
    {
        private readonly DataContext _context;
        private LoanAssetRepository _loanAssetRepository;
        private IMapper _mapper;

        public LoanAssetController(DataContext context, IMapper mapper, LoanAssetRepository loanAssetRepository)
        {
            _context = context;
            _mapper = mapper;
            _loanAssetRepository = loanAssetRepository;
        }


        [HttpGet]
        [Authorize(Roles = "Suporte")]
        public async Task<ActionResult<IEnumerable<LoanAssetModel>>> GetLoanAssetList()
        {
            return await _context.loanAsset.ToListAsync();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public async Task<ActionResult<LoanAssetModel>> ObterPorId(int id)
        {
            return _loanAssetRepository.GetByID(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<IActionResult> UpdateLoanAssetModel(string id, LoanAssetModel laonAssetModel)
        {
            throw new Exception();
        }


        [HttpPost("Create")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<ActionResult<LoanAssetModel>> CreateLoanAssetModel(CreateLoanAsset loanAsset)
        {
            LoanAssetModel loanAssetModel = _mapper.Map<CreateLoanAsset, LoanAssetModel>(loanAsset);
            var resultado = _loanAssetRepository.CreateLoanAsset(loanAssetModel);

            if (resultado.Status)
            {
                return Ok("Registrado com sucesso");
            }
            return BadRequest(resultado.Mensagem);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<IActionResult> DeleteLoanAssetModel(string id)
        {
            throw new Exception();
        }

        [HttpGet("ListarPorCompanhia/{idCompany}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult CompanyLoanAssetsList(int idCompany)
        {
            var loanList = _loanAssetRepository.CompanyLoanAssetsList(idCompany);
            
            if (loanList.Any())
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
        public IActionResult TerminationLoanAsset(int id, TerminationLoanAssetViewModel terminationLoanAsset)
        {
            try
            {
                var loanAsset = _loanAssetRepository.GetByID(id);
                loanAsset.DevolutionDate = terminationLoanAsset.Date ?? DateTime.Now;
                loanAsset.Description = terminationLoanAsset.Description;
                return Ok("Termino de Contrato Registrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private bool LoanAssetModelExists(string id)
        {
            return _context.loanAsset.Any(e => e.IdLoanAsset == id);
        }
    }
}
