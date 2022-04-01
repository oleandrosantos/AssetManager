﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;
using AssetManager.Interfaces;

namespace AssetManager
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationAssetController : ControllerBase
    {
        private readonly DataContext _context;
        private ILocationAssetService _locationAssetService;

        public LocationAssetController(DataContext context, ILocationAssetService locationAssetService)
        {
            _context = context;
            _locationAssetService = locationAssetService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationAssetModel>>> GetlocationAsset()
        {
            return await _context.locationAsset.ToListAsync();
        }


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


        [HttpPut("{id}")]
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
        public async Task<ActionResult<LocationAssetModel>> PostLocationAssetModel(CreateLocationAsset locationAsset)
        {
            _locationAssetService.CreateLocationAsset(locationAsset);
        
            return Ok("Location Criada Com Sucesso");
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
