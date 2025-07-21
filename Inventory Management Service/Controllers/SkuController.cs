using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Management_Service;

namespace Inventory_Management_Service.Controllers
{
    [ApiController]
    [Route("api")] // Route will be specified per action
    public class SkuController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        public SkuController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/products/{productId}/skus
        [HttpGet("products/{productId}/skus")]
        public async Task<IActionResult> GetSkusForProduct(int productId)
        {
            var productExists = await _context.Products.AnyAsync(p => p.Id == productId);
            if (!productExists)
                return NotFound("Product not found");
            var skus = await _context.SKUs.Where(s => s.ProductId == productId).ToListAsync();
            return Ok(skus);
        }

        // POST: api/products/{productId}/skus
        [HttpPost("products/{productId}/skus")]
        public async Task<IActionResult> CreateSku(int productId, [FromBody] SKU sku)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound("Product not found");
            var codeExists = await _context.SKUs.AnyAsync(s => s.Code == sku.Code);
            if (codeExists)
                return BadRequest("SKU code must be unique");
            sku.ProductId = productId;
            _context.SKUs.Add(sku);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSku), new { id = sku.Id }, sku);
        }

        // GET: api/skus/{id}
        [HttpGet("skus/{id}")]
        public async Task<IActionResult> GetSku(int id)
        {
            var sku = await _context.SKUs.Include(s => s.Product).FirstOrDefaultAsync(s => s.Id == id);
            if (sku == null)
                return NotFound();
            return Ok(sku);
        }

        // PUT: api/skus/{id}
        [HttpPut("skus/{id}")]
        public async Task<IActionResult> UpdateSku(int id, [FromBody] SKU sku)
        {
            if (id != sku.Id)
                return BadRequest("ID mismatch");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var exists = await _context.SKUs.AnyAsync(s => s.Id == id);
            if (!exists)
                return NotFound();
            var codeExists = await _context.SKUs.AnyAsync(s => s.Code == sku.Code && s.Id != id);
            if (codeExists)
                return BadRequest("SKU code must be unique");
            _context.Entry(sku).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/skus/{id}
        [HttpDelete("skus/{id}")]
        public async Task<IActionResult> DeleteSku(int id)
        {
            var sku = await _context.SKUs.FindAsync(id);
            if (sku == null)
                return NotFound();
            _context.SKUs.Remove(sku);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
