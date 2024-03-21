namespace EShop.Application.Order;

public interface IOrderService
{
    Task<List<GetAllOrdersDto>> GetAllOrdersAsync();
    Task<List<Domain.Models.Order>> GetOrdersByUserIdAsync(Guid userId);
    Task CreateOrderAsync(Guid userId, Guid basketId, string address);
    Task DeleteOrder (Guid orderId);
}