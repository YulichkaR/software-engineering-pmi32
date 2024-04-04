using System.Security.Claims;
using EShop.Application.Basket;
using EShop.Presentation.Models.Basket;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) {
            return RedirectToAction("Login", "Auth");
        }
        var basket = await _basketService.GetUserBasket(new Guid(userId));
        var basketViewModel = new BasketViewModel
        {
            Id = basket.Id,
            UserId = basket.UserId,
            TotalPrice = basket.TotalPrice,
            Items = basket.Items
        };
        return View(basketViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem(Guid productId, decimal price, int quantity)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) {
            return RedirectToAction("Login", "Auth");
        }
        await _basketService.AddItemToBasket(new Guid(userId), productId, price, quantity);
        return RedirectToAction("Index");
    }
}