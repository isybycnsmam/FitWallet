using System.Linq.Expressions;
using AutoMapper;

namespace FitWallet.Api.Extensions;

public static class MappingExpressionExtensions
{
    public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> map,
        Expression<Func<TDestination, object>> selector)
    {
        map.ForMember(selector, config => config.Ignore());
        return map;
    }
}