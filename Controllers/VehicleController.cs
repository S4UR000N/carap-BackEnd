using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api_storm.Data;
using api_storm.Models.DatabaseModels;
using api_storm.Models.CLResponseModels;
using api_storm.Models.DBCommandModels;

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
            var vehicles = await _context.VehicleModel
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.VehicleType)
                    .Select(x => new CLRVehicleModel
                    {
                        Id = x.Id,
                        BrandName = x.Brand.Name,
                        VehicleTypeName = x.VehicleType.Name,
                        ModelName = x.ModelName
                    })
                .ToListAsync()
            ;

            return Ok(vehicles);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetVehicleById(int id)
        {
            var vehicle = await _context.VehicleModel
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.VehicleType)
                .Where(x => x.Id == id)
                    .Select(x => new CLRVehicleModel
                    {
                        Id = x.Id,
                        BrandName = x.Brand.Name,
                        VehicleTypeName = x.VehicleType.Name,
                        ModelName = x.ModelName
                    })
                .ToListAsync()
            ;
            if (vehicle == null || !vehicle.Any())
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpGet("{modelName}")]
        public async Task<ActionResult> GetVehicleByModelName(string modelName)
        {
            var vehicle = await _context.VehicleModel
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.VehicleType)
                    .Select(x => new CLRVehicleModel
                    {
                        Id = x.Id,
                        BrandName = x.Brand.Name,
                        VehicleTypeName = x.VehicleType.Name,
                        ModelName = x.ModelName
                    })
                .FirstOrDefaultAsync(m => m.ModelName == modelName)
            ;
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }


        [HttpPost]
        public async Task<ActionResult> CreateVehicle([FromBody] DBCVehicleModel dbcVehicleModel)
        {
            if (!string.IsNullOrEmpty(dbcVehicleModel.ModelName))
            {
                var vehicleModel = new VehicleModel()
                {
                    BrandId = dbcVehicleModel.BrandId,
                    VehicleTypeId = dbcVehicleModel.VehicleTypeId,
                    ModelName = dbcVehicleModel.ModelName
                };
                _context.Add<VehicleModel>(vehicleModel);
                await _context.SaveChangesAsync();
                return Ok($"{vehicleModel.ModelName} Saved Successfuly!");
            }
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateVehicle(int id, [FromBody] DBCVehicleModel dbcVehicleModel)
        {
            // Create new model and get existing one from DB
            var vehicleModel = new VehicleModel();
            var vehicle = await _context.VehicleModel.FindAsync(id);
            _context.ChangeTracker.Clear();

            // Validation
            vehicleModel.Id = (id > 0) ? id : vehicle.Id;
            vehicleModel.BrandId = (dbcVehicleModel.BrandId > 0) ? dbcVehicleModel.BrandId : vehicle.BrandId;
            vehicleModel.VehicleTypeId = (dbcVehicleModel.VehicleTypeId > 0) ? dbcVehicleModel.VehicleTypeId  : vehicle.VehicleTypeId;
            vehicleModel.ModelName = (!string.IsNullOrWhiteSpace(dbcVehicleModel.ModelName)) ? dbcVehicleModel.ModelName : vehicle.ModelName;

            // Save or throw error
            _context.Entry(vehicleModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleModelExists(vehicleModel.Id))
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
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var vehicleModel = await _context.VehicleModel.FindAsync(id);
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
            return _context.VehicleModel.Any(e => e.Id == id);
        }
    }
}
