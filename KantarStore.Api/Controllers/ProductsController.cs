using Microsoft.AspNetCore.Mvc;
using KantarStore.Application.Products;
using KantarStore.Application.Dtos;

namespace KantarStore.Api.Controllers
{
    [ApiController]
    [Route("KantarStore/api/Products")]
    public class ProductsController (IProductsService productsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coll = await productsService.GetAllProducts();
            return Ok(coll);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var product = await productsService.GetById(id);

            if(product == null)
                return NotFound();
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody]NewProductDto newProduct)
        {
            Guid guid = await productsService.AddProduct(newProduct);

            if (guid == Guid.Empty)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(GetById), new{id = guid}, null);
        }
    }
}
