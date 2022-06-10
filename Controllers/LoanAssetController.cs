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
        private AssetRepository _assetRepository;
        private UserRepository _userRepository;
        private IMapper _mapper;

        public LoanAssetController(DataContext context, IMapper mapper, LoanAssetRepository loanAssetRepository)
        {
            _context = context;
            _mapper = mapper;
            _loanAssetRepository = loanAssetRepository;
        }


        [HttpGet]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public async Task<ActionResult<IEnumerable<LoanAssetModel>>> GetLoanAssetList()
        {
            return await _context.loanAsset.ToListAsync();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public async Task<ActionResult<LoanAssetModel>> GetLoanAsset(int id)
        {
            return _loanAssetRepository.GetByID(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<IActionResult> PutLoanAssetModel(string id, LoanAssetModel laonAssetModel)
        {
            if (id != laonAssetModel.idLoanAsset)
            {
                return BadRequest();
            }

            _context.Entry(laonAssetModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanAssetModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost("Create")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<ActionResult<LoanAssetModel>> CreateLoanAssetModel(CreateLoanAsset loanAsset)
        {
            LoanAssetModel loanAssetModel = _mapper.Map<CreateLoanAsset, LoanAssetModel>(loanAsset);
            loanAssetModel.asset = _assetRepository.GetAssetByID(loanAsset.IdAsset);
            loanAssetModel.usuario = _userRepository.GetUserById(loanAsset.IdUsuario);

            _loanAssetRepository.CreateLoanAsset(loanAssetModel);
        
            return Ok("Registrado com sucesso");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<IActionResult> DeleteLoanAssetModel(string id)
        {
            var laonAssetModel = await _context.loanAsset.FindAsync(id);
            if (laonAssetModel == null)
            {
                return NotFound();
            }

            _context.loanAsset.Remove(laonAssetModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("ListarPorCompanhia/{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public IActionResult CompanyLoanAssetsList(int idCompany)
        {
            var loanList = _loanAssetRepository.CompanyLoanAssetsList(idCompany);
            
            if (loanList.Any())
                return Ok(loanList);

            return BadRequest();
        }
        
        [HttpGet("ListarPorUsuario/{id}")]
        [Authorize(Roles = "Administrador,Suporte, Funcionario")]
        public IActionResult UserAssetLoanList(string idUsuario)
        {
            var loanList = _loanAssetRepository.UserAssetLoanList(idUsuario, true);
            
            if (loanList.Any())
                return Ok(loanList);

            return BadRequest();
        }
        private bool LoanAssetModelExists(string id)
        {
            return _context.loanAsset.Any(e => e.idLoanAsset == id);
        }
    }
}
