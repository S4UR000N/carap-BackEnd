using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api_storm.Data;
using api_storm.Models.DatabaseModels;

namespace api_storm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly api_stormContext _context;
        public BrandController(api_stormContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBrands()
        {
            return Ok(await _context.BrandModel.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetBrandById(int id)
        {
            var brand = await _context.BrandModel.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpGet("{brandName}")]
        public async Task<ActionResult> GetBrandByName(string brandName)
        {
            var brand = await _context.BrandModel.FirstOrDefaultAsync(m => m.Name == brandName);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }


        [HttpPost]
        public async Task<ActionResult> CreateBrand([Bind(include: nameof(BrandModel.Name))][FromBody] BrandModel brandModel)
        {
            if (!String.IsNullOrEmpty(brandModel.Name))
            {
                _context.Add(brandModel);
                await _context.SaveChangesAsync();
                return Ok($"{brandModel.Name} Saved Successfuly!");
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBrand([FromBody] BrandModel brandModel)
        {
            _context.Entry(brandModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandModelExists(brandModel.Id))
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var brandModel = await _context.BrandModel.FindAsync(id);
            if (brandModel == null)
            {
                return NotFound();
            }
            _context.BrandModel.Remove(brandModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool BrandModelExists(int id)
        {
            return _context.BrandModel.Any(e => e.Id == id);
        }
    }
}
