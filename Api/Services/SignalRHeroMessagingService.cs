using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Api.Models;


namespace Api.Services
{

    /// <summary>
    /// SignalR implementation of the Hero messaging service.
    /// </summary>
    public class SignalRHeroMessagingService : IHeroMessagingService
    {

        private readonly HubConnection _connection;


        /// <summary>
        /// Creates a new SignalR stub for the Hero messaging service.
        /// </summary>
        /// <param name="options">Messaging options to use.</param>
        public SignalRHeroMessagingService(IOptions<MessagingOptions> options)
        {
            var messagingOptions = options.Value;
            var url = messagingOptions.Url;
            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            _connection
                .StartAsync()
                .Wait();
        }


        /// <summary>
        /// Creates a new SignalR stub for the Hero messaging service.
        /// This is normally used for unit testing.
        /// </summary>
        /// <param name="connection">Hub connection to use.</param>
        public SignalRHeroMessagingService(HubConnection connection)
        {
            _connection = connection;
        }


        public async Task StartAsync()
        {
            await _connection.StartAsync();
        }


        public async Task AddAsync(HeroModel addedHero)
        {
            await _connection.InvokeAsync("Add", addedHero);
        }


        public async Task UpdateAsync(HeroModel updatedHero)
        {
            await _connection.InvokeAsync("Update", updatedHero);
        }


        public async Task RemoveAsync(long id)
        {
            await _connection.InvokeAsync("Remove", id);
        }

    }

}
