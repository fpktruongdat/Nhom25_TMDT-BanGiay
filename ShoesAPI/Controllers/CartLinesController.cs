using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesAPI.Data;
using ShoesAPI.Models;

namespace ShoesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartLinesController : ControllerBase
    {
        private readonly ShopShoesDbContext _context;

        public CartLinesController(ShopShoesDbContext context)
        {
            _context = context;
        }

        // GET: api/CartLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartLine>>> GetCartLines()
        {
            return await _context.CartLines.ToListAsync();
        }

        // GET: api/CartLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartLine>> GetCartLine(int id)
        {
            var cartLine = await _context.CartLines.FindAsync(id);

            if (cartLine == null)
            {
                return NotFound();
            }

            return cartLine;
        }

        // PUT: api/CartLines/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartLine(int id, CartLine cartLine)
        {
            if (id != cartLine.ID)
            {
                return BadRequest();
            }

            _context.Entry(cartLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartLineExists(id))
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

        // POST: api/CartLines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CartLine>> PostCartLine(CartLine cartLine)
        {
            _context.CartLines.Add(cartLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartLine", new { id = cartLine.ID }, cartLine);
        }

        // DELETE: api/CartLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartLine>> DeleteCartLine(int id)
        {
            var cartLine = await _context.CartLines.FindAsync(id);
            if (cartLine == null)
            {
                return NotFound();
            }

            _context.CartLines.Remove(cartLine);
            await _context.SaveChangesAsync();

            return cartLine;
        }

        private bool CartLineExists(int id)
        {
            return _context.CartLines.Any(e => e.ID == id);
        }
    }
}
