#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationAssetController : ControllerBase
    {
        private readonly DataContext _context;

        public LocationAssetController(DataContext context)
        {
            _context = context;
        }

        // GET: api/LocationAsset
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationAssetModel>>> GetlocationAsset()
        {
            return await _context.locationAsset.ToListAsync();
        }

        // GET: api/LocationAsset/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationAssetModel>> GetLocationAssetModel(string id)
        {
            
            var locationAssetModel = await _context.locationAsset.FindAsync(id);

            if (locationAssetModel == null)
            {
                return NotFound();
            }

            return locationAssetModel;
        }

        // PUT: api/LocationAsset/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationAssetModel(string id, CreateLocationAsset locationAssetModel)
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

        // POST: api/LocationAsset
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationAssetModel>> PostLocationAssetModel(CreateLocationAsset locationAsset)
        {
            LocationAssetModel locationAssetModel = AutoMapper.Profile<CreateLocationAsset, LocationAssetModel>();
            _context.locationAsset.Add(locationAssetModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocationAssetModelExists(locationAssetModel.idLocationAsset))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLocationAssetModel", new { id = locationAssetModel.idLocationAsset }, locationAssetModel);
        }

        // DELETE: api/LocationAsset/5
        [HttpDelete("{id}")]
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
