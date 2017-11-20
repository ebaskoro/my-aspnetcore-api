using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Internal.Protocol;
using Microsoft.AspNetCore.Sockets.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FakeItEasy;
using Xunit;
using Api.Models;
using Api.Services;


namespace Api.Tests.Services
{

    public class SignalRHeroMessagingServiceTest : IDisposable
    {

        public SignalRHeroMessagingServiceTest()
        {
            var connection = A.Fake<IConnection>();
            A.CallTo(() => connection.Features)
                .Returns(new FeatureCollection());
            A.CallTo(() => connection.StartAsync())
                .Returns(Task.CompletedTask);
            A.CallTo(() => connection.DisposeAsync())
                .Returns(Task.CompletedTask);
            A.CallTo(() => connection.SendAsync(A<byte[]>.Ignored, A<CancellationToken>.Ignored))
                .Invokes(() => Console.WriteLine("Invoked"))
                .Returns(Task.CompletedTask);

            var hubProtocol = A.Fake<IHubProtocol>();
            A.CallTo(() => hubProtocol.Name)
                .Returns("Fake Protocol");

            var loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();

            Connection = A.Fake<HubConnection>(option => option
                .WithArgumentsForConstructor(new object[]
                {
                    connection,
                    hubProtocol,
                    loggerFactory
                }));
            Connection.StartAsync().Wait();

            Target = new SignalRHeroMessagingService(Connection);
        }


        HubConnection Connection
        {
            get;
        }


        /// <summary>
        /// Gets the target to test.
        /// </summary>
        SignalRHeroMessagingService Target
        {
            get;
        }


        [Fact]
        public void Constructor_Throws_Exception()
        {
            var options = A.Fake<IOptions<MessagingOptions>>();
            A.CallTo(() => options.Value)
                .Returns(new MessagingOptions
                {
                    Url = "http://localhost"
                });

            Assert.Throws<AggregateException>(() => new SignalRHeroMessagingService(options));
        }


        [Fact]
        public async void StartAsync_Throws_NoException()
        {
            await Target.StartAsync();
        }


        //[Fact]
        public async void AddAsync_Throws_NoException()
        {
            Console.WriteLine("AddAsync_Throws_NoException");
            await Target.AddAsync(new HeroModel());
        }


        //[Fact]
        public async void UpdateAsync_Throws_NoException()
        {
            await Target.UpdateAsync(new HeroModel());
        }


        //[Fact]
        public async void RemoveAsync_Throws_NoException()
        {
            await Target.RemoveAsync(0);
        }


        /// <summary>
        /// Disposes the test context.
        /// </summary>
        public void Dispose()
        {
            Connection.DisposeAsync().Wait();
        }

    }

}
