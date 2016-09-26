using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Cartoonaloge.Models.ApiViewModels;
using Cartoonalogue.Data;
using Cartoonalogue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cartoonaloge.Controllers.Api
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/cartoons")]
        public async Task<IActionResult> GetAllAsync()
        {
            var cartoons = await _context.Cartoons.Include(c => c.Studio).Include(c => c.Network).OrderBy(c => c.Name).ToListAsync();
            var viewModel = new List<CartoonsListViewModel>();
            foreach (var item in cartoons)
            {
                viewModel.Add(new CartoonsListViewModel
                {
                    Name = item.Name,
                    WhenDebuted = item.WhenDebuted,
                    Seasons = item.Seasons,
                    Studio = item.Studio.Name,
                    Network = item.Network.Name,
                    Trivia = item.Trivia
                });
            }
            
            return Ok(viewModel);
        }

        [HttpGet("/cartoon")]
        public async Task<IActionResult> GetCartoonAsync(string name)
        {
            var item = await _context.Cartoons.Include(c => c.Network).Include(c => c.Studio).FirstOrDefaultAsync(c => c.Name == name);
            if (item == null)
            {
                return NotFound("Cartoon not found");
            }

            var viewModel = new CartoonsListViewModel
            {
                Name = item.Name,
                WhenDebuted = item.WhenDebuted,
                Seasons = item.Seasons,
                Studio = item.Studio.Name,
                Network = item.Network.Name,
                Trivia = item.Trivia
            };
            return Ok(viewModel);

        }

        [HttpPost("/cartoon")]
        public async Task<IActionResult> PostAsync([FromBody] Cartoon cartoon)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(cartoon);
                    await _context.SaveChangesAsync();
                    return Created($"api/cartoon/{cartoon.Name}", cartoon);
                }
                catch (Exception ex)
                {
                    // TODO: Log error
                    return BadRequest("Failed to save the cartoon");
                }
            }
            else
            {
                return BadRequest("Bad data");
            }
        }
    }
}
