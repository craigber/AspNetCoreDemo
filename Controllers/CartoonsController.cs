using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cartoonalogue.Data;
using Cartoonalogue.Models;
using Microsoft.Extensions.Logging;

namespace Cartoonalogue.Controllers
{
    public class CartoonsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartoonsController> _logger;

        public CartoonsController(ApplicationDbContext context, ILogger<CartoonsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Cartoons
        public async Task<IActionResult> Index()
        {
            try
            {
                var cartoons = await _context.Cartoons.Include(c => c.Studio).Include(c => c.Network).OrderBy(c => c.Name).ToListAsync();
                return View(cartoons);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve cartoons in Index page: {ex.Message}");
                return Redirect("/error");
            }
        }

        // GET: Cartoons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartoon = await _context.Cartoons.Include(c => c.Network).Include(c => c.Studio).SingleOrDefaultAsync(m => m.Id == id);
            if (cartoon == null)
            {
                return NotFound();
            }

            return View(cartoon);
        }

        // GET: Cartoons/Create
        public IActionResult Create()
        {
            ViewData["StudioId"] = new SelectList(_context.Studios.OrderBy(s => s.Name), "Id", "Name");
            ViewData["NetworkId"] = new SelectList(_context.Networks.OrderBy(s => s.Name), "Id", "Name");
            return View();
        }

        // POST: Cartoons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Network,Seasons,StudioId,WhenDebuted,NetworkId,Trivia")] Cartoon cartoon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartoon);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["StudioId"] = new SelectList(_context.Studios.OrderBy(s => s.Name), "Id", "Name");
            ViewData["NetworkId"] = new SelectList(_context.Networks.OrderBy(s => s.Name), "Id", "Name");
            return View(cartoon);
        }

        // GET: Cartoons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartoon = await _context.Cartoons.SingleOrDefaultAsync(m => m.Id == id);
            if (cartoon == null)
            {
                return NotFound();
            }
            ViewData["StudioId"] = new SelectList(_context.Studios.OrderBy(s => s.Name), "Id", "Name");
            ViewData["NetworkId"] = new SelectList(_context.Networks.OrderBy(s => s.Name), "Id", "Name");
            return View(cartoon);
        }

        // POST: Cartoons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Network,Seasons,StudioId,WhenDebuted,NetworkId,Trivia")] Cartoon cartoon)
        {
            if (id != cartoon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartoon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartoonExists(cartoon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["StudioId"] = new SelectList(_context.Studios.OrderBy(s => s.Name), "Id", "Name");
            ViewData["NetworkId"] = new SelectList(_context.Networks.OrderBy(s => s.Name), "Id", "Name");
            return View(cartoon);
        }

        // GET: Cartoons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartoon = await _context.Cartoons.Include(c => c.Network).Include(c => c.Studio).FirstOrDefaultAsync(m => m.Id == id);
            if (cartoon == null)
            {
                return NotFound();
            }

            return View(cartoon);
        }

        // POST: Cartoons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartoon = await _context.Cartoons.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cartoons.Remove(cartoon);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CartoonExists(int id)
        {
            return _context.Cartoons.Any(e => e.Id == id);
        }
    }
}
