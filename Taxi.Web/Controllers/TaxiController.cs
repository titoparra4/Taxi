using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Taxi.Web.Data;
using Taxi.Web.Data.Entities;

namespace Taxi.Web.Controllers
{
    public class TaxiController : Controller
    {
        private readonly DataContext _context;

        public TaxiController(DataContext context)
        {
            _context = context;
        }

        // GET: Taxi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Taxis.ToListAsync());
        }

        // GET: Taxi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiEntity = await _context.Taxis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxiEntity == null)
            {
                return NotFound();
            }

            return View(taxiEntity);
        }

        // GET: Taxi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Taxi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Plaque")] TaxiEntity taxiEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxiEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxiEntity);
        }

        // GET: Taxi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiEntity = await _context.Taxis.FindAsync(id);
            if (taxiEntity == null)
            {
                return NotFound();
            }
            return View(taxiEntity);
        }

        // POST: Taxi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Plaque")] TaxiEntity taxiEntity)
        {
            if (id != taxiEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxiEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxiEntityExists(taxiEntity.Id))
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
            return View(taxiEntity);
        }

        // GET: Taxi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiEntity = await _context.Taxis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxiEntity == null)
            {
                return NotFound();
            }

            return View(taxiEntity);
        }

        // POST: Taxi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxiEntity = await _context.Taxis.FindAsync(id);
            _context.Taxis.Remove(taxiEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxiEntityExists(int id)
        {
            return _context.Taxis.Any(e => e.Id == id);
        }
    }
}
