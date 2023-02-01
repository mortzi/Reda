using Microsoft.Extensions.Options;

namespace Reda.Application.Cache;

public class CacheOptions : IOptions<CacheOptions>
{
    public TimeSpan DefaultExpirationTimeSpan { get; set; } = default!;
    
    public CacheOptions Value => this;
}