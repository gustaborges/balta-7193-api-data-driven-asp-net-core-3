using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("categories")]
    public class CategoryController :  ControllerBase
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "get";
        }

        [HttpGet]
        [Route("{id:int}")] // : introduz restrição na rota
        public string Get(int id)
        {
            return "get " + id;
        }

        [HttpPost]
        [Route("")]
        public Category Post([FromBody] Category model)
        {
            return model;
        }

        [HttpPut]
        [Route("{id:int}")]
        public Category Put(int id, Category model)
        {
            if(model.Id == id)
                return model;

            return null;
        }

        [HttpDelete]
        [Route("")]
        public string Delete(int id)
        {
            return "delete";
        }
        
    }
}