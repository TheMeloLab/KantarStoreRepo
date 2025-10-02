using KantarStore.Application.Products;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KantarStore.Api.Controllers
{
    [Route("KantarStore/api/[controller]")]
    [ApiController]
    public class BasketController(IBasketService basketService) : ControllerBase
    {
        [HttpGet("History/{userid}")]
        public IEnumerable<string> GetUserBasketHistory(Guid userid)
        {
            return new string[] { "value1", "value2" };
        }
        
        [HttpGet("{userid}")]
        public string GetUserBasket(Guid userid)
        {
            return "value";
        }

        [HttpPost("AddToBasket")]
        public void AddToBasket([FromBody] string value)
        {
        }

        [HttpPost("RemoveFromBasket")]
        public void RemoveFromBasket([FromBody] string value)
        {
        }

        [HttpPost("Checkout")]
        public void Checkout([FromBody] string value)
        {
        }
    }
}
