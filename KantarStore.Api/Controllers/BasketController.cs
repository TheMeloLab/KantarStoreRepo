using KantarStore.Application.Dtos;
using KantarStore.Application.Services.Baskets;
using KantarStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KantarStore.Api.Controllers
{
    [Route("KantarStore/api/[controller]")]
    [ApiController]
    public class BasketsController(IBasketService basketService) : ControllerBase
    {
        [HttpGet("History/{userid}")]
        public async Task<IActionResult> GetUserBasketHistory(Guid userid)
        {
            var coll = await basketService.GetUserBasketHistory(userid);
            return Ok(coll);
        }
        
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUserBasket(Guid userid)
        {
            var coll = await basketService.GetUserBasket(userid);
            return Ok(coll);
        }

        [HttpPost("AddToBasket")]
        public async Task<IActionResult> AddToBasket([FromBody] BasketItemDto value)
        {
            var basket = await basketService.AddToBasket(value.UserId,value.ProductId,value.Quantity);
            return Ok(basket);
        }

        [HttpPost("RemoveFromBasket")]
        public async Task<IActionResult> RemoveFromBasket([FromBody] BasketItemDto value)
        {
            var basket = await basketService.RemoveFromBasket(value.UserId, value.ProductId, value.Quantity);
            return Ok(basket);
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout([FromBody] Guid userId)
        {
           var val = await basketService.Checkout(userId);
           return Ok(val);
        }
    }
}
