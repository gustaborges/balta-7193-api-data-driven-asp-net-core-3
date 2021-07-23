using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("categories")]
    public class CategoryController :  ControllerBase
    {
        private DataContext _context;

        public CategoryController([FromServices] DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            try
            {
                return await _context.Categories.AsNoTracking().ToListAsync();
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível obter categorias"});
            }
        }

        [HttpGet]
        [Route("{id:int}")] // : introduz restrição na rota
        public async Task<ActionResult<Category>> Get(int id)
        {
            try
            {
                Category category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if(category is null)
                    return NotFound(new { message = $"Categoria {id} não encontrada" });

                return Ok(category);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível obter categorias"});
            }        
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromBody] Category model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Categories.Add(model);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = model.Id}, model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar a categoria"});
            }

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category model)
        {
            if(model.Id != id)
                return NotFound(new { message = "Categoria não encontrada" });

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Categories.Update(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch(Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar a categoria"});
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                
                if(category is null)
                    return NotFound(new { message = "Categoria não encontrada" });

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Categoria removida"});
            }
            catch(Exception)
            {
                return BadRequest(new { message = "Não foi possível deletar a categoria"});
            }
        }
        
    }
}