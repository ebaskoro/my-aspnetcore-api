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


        /// <summary>
        /// Gets a hero by its ID.
        /// </summary>
        /// <param name="id">ID to look up.</param>
        /// <returns>The hero if found or NotFound otherwise.</returns>
        [HttpGet("{id}", Name = "GetHeroById")]
        public async Task<IActionResult> GetById(long id)
        {
            var foundHero = await _context
                .Heroes
                .FirstOrDefaultAsync(hero => hero.Id == id);

            if (foundHero == null)
            {
                return NotFound();
            }

            return Ok(foundHero);
        }


        /// <summary>
        /// Creates a hero.
        /// </summary>
        /// <param name="hero">Hero to create.</param>
        /// <returns>The newly created hero.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Hero hero)
        {
            if (hero == null)
            {
                return BadRequest();
            }

            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetHeroById", new { id = hero.Id }, hero);
        }


        /// <summary>
        /// Updates a hero.
        /// </summary>
        /// <param name="id">ID of the hero to update.</param>
        /// <param name="heroToUpdate">Hero to update.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Hero heroToUpdate)
        {
            if (heroToUpdate == null
                || heroToUpdate.Id != id)
            {
                return BadRequest();
            }

            var foundHero = await _context
                .Heroes
                .FirstOrDefaultAsync(hero => hero.Id == id);
            
            if (foundHero == null)
            {
                return NotFound();
            }

            foundHero.Name = heroToUpdate.Name;
            _context.Heroes.Update(foundHero);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
    
}
