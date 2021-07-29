using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using api_storm.Data;
using api_storm.Models;

namespace api_storm.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly api_stormContext _context;

        public VehicleTypeController(api_stormContext context)
        {
            _context = context;
        }

        // GET: VehicleType
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleTypeModel.ToListAsync());
        }

        // GET: VehicleType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleTypeModel = await _context.VehicleTypeModel
                .FirstOrDefaultAsync(m => m.VehicleTypeId == id);
            if (vehicleTypeModel == null)
            {
                return NotFound();
            }

            return View(vehicleTypeModel);
        }

        // GET: VehicleType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleTypeId,VehicleType")] VehicleTypeModel vehicleTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleTypeModel);
        }

        // GET: VehicleType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleTypeModel = await _context.VehicleTypeModel.FindAsync(id);
            if (vehicleTypeModel == null)
            {
                return NotFound();
            }
            return View(vehicleTypeModel);
        }

        // POST: VehicleType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleTypeId,VehicleType")] VehicleTypeModel vehicleTypeModel)
        {
            if (id != vehicleTypeModel.VehicleTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleTypeModel);
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
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleTypeModel);
        }

        // GET: VehicleType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleTypeModel = await _context.VehicleTypeModel
                .FirstOrDefaultAsync(m => m.VehicleTypeId == id);
            if (vehicleTypeModel == null)
            {
                return NotFound();
            }

            return View(vehicleTypeModel);
        }

        // POST: VehicleType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleTypeModel = await _context.VehicleTypeModel.FindAsync(id);
            _context.VehicleTypeModel.Remove(vehicleTypeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleTypeModelExists(int id)
        {
            return _context.VehicleTypeModel.Any(e => e.VehicleTypeId == id);
        }
    }
}
