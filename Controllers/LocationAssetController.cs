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
    public class LocationAssetController : ControllerBase
    {
        private readonly DataContext _context;
        private LocationAssetRepository _locationAssetRepository;
        private IMapper _mapper;

        public LocationAssetController(DataContext context, IMapper mapper, LocationAssetRepository locationAssetRepository)
        {
            _context = context;
            _mapper = mapper;
            _locationAssetRepository = locationAssetRepository;
        }


        [HttpGet]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public async Task<ActionResult<IEnumerable<LocationAssetModel>>> GetlocationAsset()
        {
            return await _context.locationAsset.ToListAsync();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Suporte,Funcionario")]
        public async Task<ActionResult<LocationAssetModel>> GetLocationAssetModel(string id)
        {
            
            var locationAssetModel = await _context.locationAsset.FindAsync(id);

            if (locationAssetModel == null)
            {
                return NotFound();
            }

            return locationAssetModel;
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<IActionResult> PutLocationAssetModel(string id, LocationAssetModel locationAssetModel)
        {
            if (id != locationAssetModel.idLocationAsset)
            {
                return BadRequest();
            }

            _context.Entry(locationAssetModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationAssetModelExists(id))
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


        [HttpPost]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<ActionResult<LocationAssetModel>> PostLocationAssetModel(CreateLocationAsset locationAsset)
        {
            LocationAssetModel locationAssetModel = _mapper.Map<CreateLocationAsset, LocationAssetModel>(locationAsset);
            locationAssetModel.asset.idAsset = locationAsset.idAsset;
            locationAssetModel.usuario.idUsuario = locationAsset.idUsuario;

            _locationAssetRepository.CreateLocationAsset(locationAssetModel);
        
            return Ok("Location Criada Com Sucesso");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador,Suporte")]
        public async Task<IActionResult> DeleteLocationAssetModel(string id)
        {
            var locationAssetModel = await _context.locationAsset.FindAsync(id);
            if (locationAssetModel == null)
            {
                return NotFound();
            }

            _context.locationAsset.Remove(locationAssetModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationAssetModelExists(string id)
        {
            return _context.locationAsset.Any(e => e.idLocationAsset == id);
        }
    }
}
