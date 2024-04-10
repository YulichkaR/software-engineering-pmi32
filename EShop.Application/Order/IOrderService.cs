using EShop.Domain.Enums;

namespace EShop.Application.Order;

public interface IOrderService
{
    Task<List<GetAllOrdersDto>> GetAllOrdersAsync();
    Task<List<GetAllOrdersDto>> GetOrdersByUserIdAsync(Guid userId);
    Task CreateOrderAsync(CreateOrderDto createOrderDto);
    public Task ChangeOrderStatus(Guid orderId, Status status);
    Task DeleteOrder (Guid orderId);
}