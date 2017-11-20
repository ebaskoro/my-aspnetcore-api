using System.Threading.Tasks;
using Api.Models;


namespace Api.Services
{

    public interface IHeroMessagingService
    {

        Task StartAsync();


        Task AddAsync(HeroModel addedHero);


        Task UpdateAsync(HeroModel updatedHero);


        Task RemoveAsync(long id);

    }

}
