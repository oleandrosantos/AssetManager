using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManager.Data;
using AssetManager.Model;
using Microsoft.AspNetCore.Authorization;

namespace AssetManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Company
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> Getcompany()
        {
            return await _context.company.ToListAsync();
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Funcionario")]
        public async Task<ActionResult<CompanyModel>> GetCompanyModel(int id)
        {
            var companyModel = await _context.company.FindAsync(id);

            if (companyModel == null)
            {
                return NotFound();
            }

            return companyModel;
        }

        // PUT: api/Company/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyModel(int id, CompanyModel companyModel)
        {
            if (id != companyModel.idCompany)
            {
                return BadRequest();
            }

            _context.Entry(companyModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyModelExists(id))
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

        // POST: api/Company
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyModel>> PostCompanyModel(CompanyModel companyModel)
        {
            _context.company.Add(companyModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyModel", new { id = companyModel.idCompany }, companyModel);
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyModel(int id)
        {
            var companyModel = await _context.company.FindAsync(id);
            if (companyModel == null)
            {
                return NotFound();
            }

            _context.company.Remove(companyModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyModelExists(int id)
        {
            return _context.company.Any(e => e.idCompany == id);
        }
    }
}
