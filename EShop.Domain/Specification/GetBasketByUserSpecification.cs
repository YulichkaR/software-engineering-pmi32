using System.Linq.Expressions;
using EShop.Domain.Abstractions;
using EShop.Domain.Models;

namespace EShop.Domain.Specification;

public class GetBasketByUserSpecification : Specification<Basket>
{
    public GetBasketByUserSpecification(Expression<Func<Basket, bool>>? criteria) : base(criteria)
    {
    }

    public GetBasketByUserSpecification(Specification<Basket> specification) : base(specification)
    {
    }
    public GetBasketByUserSpecification(Guid userId) : base(b => b.UserId == userId)
    {
        AddInclude($"{nameof(Basket.Items)}.{nameof(BasketItem.Product)}" );
        
    }
}