using Microsoft.AspNetCore.Mvc;
using veeb2.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using veeb2.Data;

namespace veeb2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TootedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TootedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/tooted
        [HttpGet]
        public List<Tooted> Get()
        {
            var toodes = _context.Toodes.ToList();
            return toodes;
        }

        // DELETE api/tooted/kustuta/1
        [HttpDelete("kustuta/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var toode = await _context.Toodes.FindAsync(id);

                if (toode == null)
                {
                    return NotFound();
                }

                _context.Toodes.Remove(toode);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Здесь можно логировать исключение для получения дополнительной информации.
                // Логирование поможет определить, что вызывает ошибку.
                return StatusCode(500, "Internal Server Error");
            }
        }



        // POST api/tooted/lisa
        /*
        [HttpPost("lisa")]
        public async Task<ActionResult<Toode>> AddToode([FromBody] Toode toode)
        {
            if (toode == null)
            {
                return BadRequest("Invalid data");
            }

            _context.Tooted.Add(toode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = toode.Id }, toode);
        }*/

        [HttpPost]
        public List<Tooted> PostCategorys([FromBody] Tooted categorys)
        {
            _context.Toodes.Add(categorys);
            _context.SaveChanges();
            return _context.Toodes.ToList();
        }

        // PATCH api/tooted/hind-dollaritesse/1.5
        [HttpPatch("hind-dollaritesse/{kurss}")]
        public async Task<IActionResult> UpdatePrices(double kurss)
        {
            var tooted = await _context.Toodes.ToListAsync();

            foreach (var toode in tooted)
            {
                toode.Price = toode.Price * kurss;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}