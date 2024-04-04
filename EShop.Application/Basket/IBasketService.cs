namespace EShop.Application.Basket;

public interface IBasketService
{
    Task<Domain.Models.Basket> GetUserBasket(Guid userId);
    Task<Domain.Models.Basket> AddItemToBasket(Guid userId, Guid itemId, decimal price, int quantity = 1);
    Task<Domain.Models.Basket> SetQuantities(Guid basketId, Dictionary<string, int> quantities);
    Task DeleteBasketAsync(Guid basketId);
}
