using EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;
using EnterpriseAssistant.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace EnterpriseAssistant.Application.Shared;

public class EnterpriseCreateTransactionCache
{
    private readonly EnterpriseAssistantDbContext _db;
    private readonly MemoryCache _cache;

    public EnterpriseCreateTransactionCache(IOptions<MemoryCacheOptions> options, EnterpriseAssistantDbContext db)
    {
        _db = db;
        _cache = new MemoryCache(new MemoryCacheOptions
        {
            SizeLimit = 128
        });
    }

    public EnterpriseCreateTransaction GetTransaction(Guid id)
    {
        if (_cache.TryGetValue(id, out EnterpriseCreateTransaction transaction)) return transaction;

        // todo: get from database
        var option = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = new DateTimeOffset(transaction.ExpirationTime),
            Priority = CacheItemPriority.Normal,
            Size = 1
        }.RegisterPostEvictionCallback(Persist, 1);

        _cache.Set(id, transaction, option);

        return transaction;
    }

    private void Persist<TGuid>(TGuid key, object value, EvictionReason reason, object state)
    {
        if (reason == EvictionReason.Expired)
        {
            // todo: 
        }
    }
}