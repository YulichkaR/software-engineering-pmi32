namespace EShop.Application.Order;

public interface IOrderService
{
    Task<List<GetAllOrdersDto>> GetAllOrdersAsync();
    Task<List<GetAllOrdersDto>> GetOrdersByUserIdAsync(Guid userId);
    Task CreateOrderAsync(CreateOrderDto createOrderDto);
    Task DeleteOrder (Guid orderId);
}