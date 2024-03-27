using System.Security.Claims;
using EShop.Application.Order;
using EShop.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var roleOfUser = User.FindFirst(ClaimTypes.Role)?.Value;
        var orders = new List<GetAllOrdersDto>();
        if (roleOfUser == "Admin")
        {
            orders = await _orderService.GetAllOrdersAsync();
        }
        else
        {
            var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            orders = await _orderService.GetOrdersByUserIdAsync(Guid.Parse(userId));
        }
        var orderViewModels = orders.ConvertAll(order => new OrderListViewModel
        {
            Id = order.Id,
            OrderTime = order.OrderTime,
            Address = order.Address,
            TotalPrice = order.TotalPrice,
            TotalItemsCount = order.TotalItemCount,
            OrderStatus = order.Status
        });
        return View(orderViewModels);
    }
}