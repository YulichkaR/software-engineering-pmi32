﻿using AutoMapper;
using EShop.Application.Basket;
using EShop.Application.User;
using EShop.Domain.Specification;
using Microsoft.AspNetCore.Identity;

namespace EShop.Application.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly UserManager<Domain.Models.User> _userManager;
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, UserManager<Domain.Models.User> userManager, IBasketRepository basketRepository,IMapper mapper)
    {
        _repository = repository;
        _userManager = userManager;
        _basketRepository = basketRepository;
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

    public async Task<List<GetAllOrdersDto>> GetOrdersByUserIdAsync(Guid userId)
    {
        var orders = await _repository.GetAllBySpecification(new GetUserOrdersSpecification(userId));
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

    public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        await ThrowIfIncorrectParameters(createOrderDto.UserId, createOrderDto.BasketId);
        var order = _mapper.Map<Domain.Models.Order>(createOrderDto);
        await _repository.Create(order);
    }

    private async Task ThrowIfIncorrectParameters(Guid userId, Guid basketId)
    {
        if(await _userManager.FindByIdAsync(userId.ToString()) is null)
        {
            throw new Exception("User not found");
        }

        if (!await _basketRepository.AnyAsync(basketId))
        { 
            throw new Exception("Basket not found");
        }
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