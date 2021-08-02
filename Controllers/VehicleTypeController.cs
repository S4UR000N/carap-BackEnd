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
    public class VehicleTypeController : ControllerBase
    {
        private readonly api_stormContext _context;
        public VehicleTypeController(api_stormContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVehicleTypes()
        {
            return Ok(await _context.VehicleTypeModel.ToListAsync());
        }

        [HttpGet("{vehicleTypeId:int}")]
        public async Task<ActionResult> GetVehicleTypeById(int vehicleTypeId)
        {
            var vehicleType = await _context.VehicleTypeModel.FindAsync(vehicleTypeId);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return Ok(vehicleType);
        }

        [HttpGet("{vehicleTypeName}")]
        public async Task<ActionResult> GetVehicleTypeByVehicleTypeName(string vehicleTypeName)
        {
            var vehicleType = await _context.VehicleTypeModel.FirstOrDefaultAsync(m => m.VehicleTypeName == vehicleTypeName);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return Ok(vehicleType);
        }


        [HttpPost]
        public async Task<ActionResult> CreateVehicleType([Bind(include: nameof(VehicleTypeModel.VehicleTypeName))][FromBody] VehicleTypeModel vehicleTypeModel)
        {
            if (!String.IsNullOrEmpty(vehicleTypeModel.VehicleTypeName))
            {
                _context.Add(vehicleTypeModel);
                await _context.SaveChangesAsync();
                return Ok($"{vehicleTypeModel.VehicleTypeName} Saved Successfuly!");
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVehicleType([FromBody] VehicleTypeModel vehicleTypeModel)
        {
            _context.Entry(vehicleTypeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleTypeModelExists(vehicleTypeModel.VehicleTypeId))
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

        [HttpDelete("{vehicleTypeId:int}")]
        public async Task<ActionResult> DeleteVehicleType(int vehicleTypeId)
        {
            var vehicleTypeModel = await _context.VehicleTypeModel.FindAsync(vehicleTypeId);
            if (vehicleTypeModel == null)
            {
                return NotFound();
            }
            _context.VehicleTypeModel.Remove(vehicleTypeModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool VehicleTypeModelExists(int id)
        {
            return _context.VehicleTypeModel.Any(e => e.VehicleTypeId == id);
        }
    }
}
