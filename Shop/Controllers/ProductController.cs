using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private DataContext _context;

        public ProductController([FromServices] DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                return await _context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível obter os produtos"});
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(x => x.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if(product is null)
                    return NotFound(new { message = "Produto " + id + " não encontrado"});

                return Ok(product);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível obter o produto " + id});
            }
        }


        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory(int id)
        {
            try
            {
                var products = await _context.Products
                    .Include(x => x.Category)
                    .AsNoTracking()
                    .Where(x => x.CategoryId == id)
                    .ToListAsync();

                return Ok(products);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível obter os produtos da categoria " + id});
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromBody] Product model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível adicionar o produto"});
            }
        }
    }
}