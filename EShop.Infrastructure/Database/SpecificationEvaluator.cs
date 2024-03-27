﻿using EShop.Domain.Abstractions;
using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Database;

public class SpecificationEvaluator<TKey, TEntity> where TEntity : class,IBaseEntity<TKey>
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, Specification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.Includes.Any())
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        
        if (spec.IncludeStrings.Any())
            query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
        
        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsSplitQuery is not null && spec.IsSplitQuery is true)
        {
            query.AsSplitQuery();
        }
        
        if (spec.PageNumber > 0 && spec.PageSize > 0)
        {
            query = query.Skip((spec.PageNumber - 1) * spec.PageSize).Take(spec.PageSize);
        }
        return query;
    }
}