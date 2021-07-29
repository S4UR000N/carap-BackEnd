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
    public class CopyBrandController : Controller
    {
        private readonly api_stormContext _context;

        public CopyBrandController(api_stormContext context)
        {
            _context = context;
        }

        // GET: Brand
        public async Task<IActionResult> Index()
        {
            return View(await _context.BrandModel.ToListAsync());
        }

        // GET: Brand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandModel = await _context.BrandModel
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brandModel == null)
            {
                return NotFound();
            }

            return View(brandModel);
        }

        // GET: Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName")] BrandModel brandModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brandModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brandModel);
        }

        // GET: Brand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandModel = await _context.BrandModel.FindAsync(id);
            if (brandModel == null)
            {
                return NotFound();
            }
            return View(brandModel);
        }

        // POST: Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName")] BrandModel brandModel)
        {
            if (id != brandModel.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brandModel);
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
                return RedirectToAction(nameof(Index));
            }
            return View(brandModel);
        }

        // GET: Brand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandModel = await _context.BrandModel
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brandModel == null)
            {
                return NotFound();
            }

            return View(brandModel);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brandModel = await _context.BrandModel.FindAsync(id);
            _context.BrandModel.Remove(brandModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandModelExists(int id)
        {
            return _context.BrandModel.Any(e => e.BrandId == id);
        }
    }
}
