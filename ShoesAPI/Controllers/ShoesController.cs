using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesAPI.Data;
using ShoesAPI.Models;

namespace ShoesAPI.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly ShopShoesDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public ShoesController(ShopShoesDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment=appEnvironment;
        }

        // GET: api/Shoes
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Shoes>>> GetAll()
        {
            return await _context.Shoes.ToListAsync();
        }

        [HttpGet]
        [Route("GetHome")]
        public async Task<ActionResult<IEnumerable<Shoes>>> GetHome()
        {
            return await _context.Shoes.Take(10).ToListAsync();
        }
        [HttpGet]
        [Route("GetShoesByCategory/{CategoryId}")]
        public async Task<ActionResult<IEnumerable<Shoes>>> GetShoesByCategory(int CategoryId)
        {
            var shoes = await _context.Shoes.Where(s => s.CategoryId == CategoryId).ToListAsync();

            if (shoes == null)
            {
                return NotFound();
            }

            return shoes;
        }

        // GET: api/Shoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shoes>> GetBy(int id)
        {
            var shoes = await _context.Shoes.FindAsync(id);

            if (shoes == null)
            {
                return NotFound();
            }

            return shoes;
        }

        // PUT: api/Shoes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        
        public async Task<IActionResult> Update(int id, Shoes shoes)
        {
            if (id != shoes.ShoesId)
            {
                return BadRequest();
            }

            _context.Entry(shoes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoesExists(id))
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

        // POST: api/Shoes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Shoes>> Create([FromForm]ShoesModel model)
        {
           List<string> listFileName = UploadedFile(model);
            Shoes shoes = new Shoes();
            shoes.Name = model.Name;
            shoes.Description = model.Description;
            shoes.Price = model.Price;
            shoes.ImageUrl = listFileName[0];
            shoes.ImageThumbnailUrl = listFileName[1];
            shoes.IsOnSale = model.IsOnSale;
            shoes.IsOnNew = model.IsOnNew;
            shoes.IsOnStock = model.IsOnStock;
            shoes.CategoryId = model.CategoryId;


            _context.Shoes.Add(shoes);
            await _context.SaveChangesAsync();



            //return CreatedAtAction("GetShoes", new { id = shoes.ShoesId }, shoes);
            return shoes;
        }
        private List<string> UploadedFile(ShoesModel model)
        {
            List<string> listFileName = new List<string>();

            if (model.ImageUrl != null)
            {
                //if (string.IsNullOrWhiteSpace(_appEnvironment.WebRootPath))
                //{
                //    _appEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                //}
                string uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "images-shoes");

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageUrl.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageUrl.CopyTo(fileStream);
                }
                uniqueFileName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images-shoes/" + uniqueFileName;
                listFileName.Add(uniqueFileName);
            }
            if (model.ImageThumbnailUrl != null)
            {
                string uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "images-shoes");

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageUrl.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageThumbnailUrl.CopyTo(fileStream);
                }
                uniqueFileName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images-shoes/" + uniqueFileName;

                listFileName.Add(uniqueFileName);
            }
            return listFileName;


        }
        // DELETE: api/Shoes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Shoes>> Delete(int id)
        {
            var shoes = await _context.Shoes.FindAsync(id);
            if (shoes == null)
            {
                return NotFound();
            }

            _context.Shoes.Remove(shoes);
            await _context.SaveChangesAsync();

            return shoes;
        }

        private bool ShoesExists(int id)
        {
            return _context.Shoes.Any(e => e.ShoesId == id);
        }
    }
}
