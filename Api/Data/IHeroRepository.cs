using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Api.Data
{

    /// <summary>
    /// Hero repository.
    /// </summary>
    public interface IHeroRepository : IDisposable
    {

        IEnumerable<Hero> GetAll();


        Task<IEnumerable<Hero>> GetAllAsync();


        Hero GetById(long id);


        Task<Hero> GetByIdAsync(long id);


        IEnumerable<Hero> SearchByName(string nameToMatch);


        Task<IEnumerable<Hero>> SearchByNameAsync(string nameToMatch);


        void Add(Hero heroToAdd);


        Task AddAsync(Hero heroToAdd);


        void Remove(Hero heroToRemove);


        void RemoveById(long id);

    }
}
