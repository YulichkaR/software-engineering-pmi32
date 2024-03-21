using EShop.Application.Order;
using EShop.Domain.Enums;
using EShop.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Controllers;

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
        var orders = await _orderService.GetAllOrdersAsync();
        var orderViewModels = orders.ConvertAll(order => new OrderListViewModel
        {
            Id = order.Id,
            OrderTime = order.OrderTime,
            Address = order.Address,
            TotalPrice = order.TotalPrice,
            TotalItemsCount = order.TotalItemCount,
            OrderStatus = order.Status
        });
        var mockedOrderViewModels = new List<OrderListViewModel>
        {
            new OrderListViewModel
            {
                Id = Guid.NewGuid(),
                OrderTime = DateTimeOffset.Now,
                Address = "Test Address",
                TotalPrice = 100,
                TotalItemsCount = 2,
                OrderStatus = Status.Confirmed
            }
        };
        return View(mockedOrderViewModels);
    }
}