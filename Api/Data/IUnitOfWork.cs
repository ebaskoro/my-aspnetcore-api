using System;
using System.Threading.Tasks;


namespace Api.Data
{

    public interface IUnitOfWork : IDisposable
    {

        IHeroRepository HeroRepository
        {
            get;
        }

        
        void Save();


        Task SaveAsync();

    }

}
