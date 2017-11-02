using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;


namespace Api.Controllers
{

    /// <summary>
    /// Heroes controller.
    /// </summary>
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {

        private readonly HeroContext _context;


        /// <summary>
        /// Creates a new controller.
        /// </summary>
        /// <param name="context">Hero context to use.</param>
        public HeroesController(HeroContext context)
        {
            _context = context;

            if (false == _context.Heroes.Any())
            {
                _context.Heroes.Add(new Hero { Name = "Dare Devil" });
                _context.Heroes.Add(new Hero { Name = "Luke Cage" });
                _context.Heroes.Add(new Hero { Name = "Jessica Jones" });
                _context.Heroes.Add(new Hero { Name = "Iron Fist" });
                _context.Heroes.Add(new Hero { Name = "Arrow" });
                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Gets all heroes.
        /// </summary>
        /// <returns>Collection of heroes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var heroes = await _context.Heroes.ToListAsync();
            return Ok(heroes);
        }
        
    }
    
}
