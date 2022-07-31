using System.ComponentModel.DataAnnotations;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseAssistant.Web.Helpers.DataTransfer;

public abstract class PagedResult
{
    public const int DefaultPageSize = 20;

    public const int MaxPageSize = 1000;

    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }
}

public class PagedResult<T> : PagedResult
{
    public IEnumerable<T> Data { get; set; } = null!;

    public PagedResult<TResult> ToPagedResultOf<TResult>()
    {
        return new PagedResult<TResult>
        {
            Data = Data.Adapt<IEnumerable<TResult>>(),
            PageNumber = PageNumber,
            PageSize = PageSize,
            TotalCount = TotalCount
        };
    }
}

public static class QueryablePagedResultExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int pageNumber,
        int pageSize = PagedResult.DefaultPageSize)
    {
        var totalCount = await query.CountAsync();
        var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<T>
        {
            TotalCount = totalCount,
            PageSize = pageSize,
            PageNumber = pageNumber,
            Data = data
        };
    }
}

public class PageSizeAttribute : RangeAttribute
{
    public PageSizeAttribute(int maxPageSize = PagedResult.MaxPageSize) : base(1, maxPageSize)
    {
    }
}