using BingoDAL.EntityFramework;
using BingoCore.GameCardFactory;
using BingoCore.GameCardFactory.Interfaces;
using BingoCore.GameEngine;
using BingoCore.GameEngine.Interfaces;
using BingoCore.GamePlayerFactory;
using BingoCore.GamePlayerFactory.Interfaces;
using BingoServices.MapperProfile;
using BingoServices.Services;
using BingoServices.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Bingo.Extensions
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(this IServiceCollection services) 
        {
            services.AddTransient<IBingoService, BingoService>();

            services.AddTransient<ICardCreator, BingoCardCreator>();
            services.AddTransient<IPlayerCreator, BingoPlayerCreator>();
            services.AddTransient<IWinningLineCalculator, SingleLineCalculator>();
            services.AddTransient<IGameEngine, BingoGameEngine>();
            
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            services.AddAutoMapper(typeof(ServicesProfile));

            services.AddDbContext<BingoDbContext>();
            
            services.AddMemoryCache(options => new MemoryCacheOptions
            {
                 TrackLinkedCacheEntries = true
            });
        }
    }
}
