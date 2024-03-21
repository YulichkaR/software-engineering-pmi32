using AutoMapper;
using EShop.Domain.Specification;

namespace EShop.Application.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetAllOrdersDto>> GetAllOrdersAsync()
    {
        var orders = await _repository.GetAllBySpecification(new GetAllOrdersSpecification());

        var getAllOrdersDto = orders.ConvertAll(o => new GetAllOrdersDto
        {
            OrderTime = o.OrderTime,
            Address = o.Address,
            Id = o.Id,
            Status = o.Status,
            TotalItemCount = o.Basket.Items.Sum(i => i.Quantity),
            TotalPrice = o.Basket.Items.Sum(i => i.Price * i.Quantity)
        });
        
        return getAllOrdersDto;
    }

    public async Task<List<Domain.Models.Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await _repository.GetAllBySpecification(new GetUserOrdersSpecification(userId));
    }

    public Task CreateOrderAsync(Guid userId, Guid basketId, string address)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOrder(Guid orderId)
    {
        var isExist = await _repository.AnyAsync(orderId);
        if (!isExist)
        {
            throw new Exception("Order not found");
        }
        
        await _repository.Delete(orderId);
    }
}