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

        [HttpGet("{brandId:int}")]
        public async Task<ActionResult> GetBrandById(int brandId)
        {
            var brand = await _context.BrandModel.FindAsync(brandId);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpGet("{brandName}")]
        public async Task<ActionResult> GetBrandByBrandName(string brandName)
        {
            var brand = await _context.BrandModel.FirstOrDefaultAsync(m => m.BrandName == brandName);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }


        [HttpPost]
        public async Task<ActionResult> CreateBrand([Bind(include: nameof(BrandModel.BrandName))][FromBody] BrandModel brandModel)
        {
            if (!String.IsNullOrEmpty(brandModel.BrandName))
            {
                _context.Add(brandModel);
                await _context.SaveChangesAsync();
                return Ok($"{brandModel.BrandName} Saved Successfuly!");
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
                if (!BrandModelExists(brandModel.BrandId))
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

        [HttpDelete("{brandId:int}")]
        public async Task<ActionResult> DeleteBrand(int brandId)
        {
            var brandModel = await _context.BrandModel.FindAsync(brandId);
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
            return _context.BrandModel.Any(e => e.BrandId == id);
        }
    }
}
