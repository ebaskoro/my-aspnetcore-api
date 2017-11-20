using System.Threading.Tasks;
using Api.Models;


namespace Api.Services
{

    /// <summary>
    /// No-op implementation of the Hero messaging service.
    /// </summary>
    public class NoopHeroMessagingService : IHeroMessagingService
    {

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }


        public Task AddAsync(HeroModel addedHero)
        {
            return Task.CompletedTask;
        }


        public Task UpdateAsync(HeroModel updatedHero)
        {
            return Task.CompletedTask;
        }


        public Task RemoveAsync(long id)
        {
            return Task.CompletedTask;
        }

    }

}
