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
    public class VehicleController : ControllerBase
    {
        private readonly api_stormContext _context;
        public VehicleController(api_stormContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVehicles()
        {
            return Ok(await _context.VehicleModel.ToListAsync());
        }

        [HttpGet("{vehicleId:int}")]
        public async Task<ActionResult> GetVehicleById(int vehicleId)
        {
            var vehicle = await _context.VehicleModel.FindAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        [HttpGet("{vehicleName}")]
        public async Task<ActionResult> GetVehicleByVehicleName(string vehicleName)
        {
            var vehicle = await _context.VehicleModel.FirstOrDefaultAsync(m => m.VehicleName == vehicleName);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }


        [HttpPost]
        public async Task<ActionResult> CreateVehicle([Bind(include: nameof(VehicleModel.VehicleName))][FromBody] VehicleModel vehicleModel)
        {
            if (!String.IsNullOrEmpty(vehicleModel.VehicleName))
            {
                _context.Add(vehicleModel);
                await _context.SaveChangesAsync();
                return Ok($"{vehicleModel.VehicleName} Saved Successfuly!");
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVehicle([FromBody] VehicleModel vehicleModel)
        {
            _context.Entry(vehicleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleModelExists(vehicleModel.VehicleId))
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

        [HttpDelete("{vehicleId:int}")]
        public async Task<ActionResult> DeleteVehicle(int vehicleId)
        {
            var vehicleModel = await _context.VehicleModel.FindAsync(vehicleId);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            _context.VehicleModel.Remove(vehicleModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool VehicleModelExists(int id)
        {
            return _context.VehicleModel.Any(e => e.VehicleId == id);
        }
    }
}
