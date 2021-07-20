using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("categories")]
    public class CategoryController :  ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return new List<Category>();
        }

        [HttpGet]
        [Route("{id:int}")] // : introduz restrição na rota
        public async Task<ActionResult<Category>> Get(int id)
        {
            return new Category();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromBody] Category model)
        {
            return CreatedAtAction(nameof(Get), new { id = model.Id}, model);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category model)
        {
            if(model.Id == id)
                return Ok(model);

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok();
        }
        
    }
}