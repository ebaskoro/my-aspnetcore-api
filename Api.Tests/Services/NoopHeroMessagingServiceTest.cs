using System;
using Xunit;
using Api.Services;


namespace Api.Tests.Services
{

    public class NoopHeroMessagingServiceTest : IDisposable
    {

        public NoopHeroMessagingServiceTest()
        {
            Target = new NoopHeroMessagingService();
        }


        /// <summary>
        /// Gets the target to test.
        /// </summary>
        NoopHeroMessagingService Target
        {
            get;
        }


        [Fact]
        public async void StartAsync_Throws_NoException()
        {
            await Target.StartAsync();
        }


        [Fact]
        public async void AddAsync_Throws_NoException()
        {
            await Target.AddAsync(null);
        }


        [Fact]
        public async void UpdateAsync_Throws_NoException()
        {
            await Target.UpdateAsync(null);
        }


        [Fact]
        public async void RemoveAsync_Throws_NoException()
        {
            await Target.RemoveAsync(0);
        }
        
        
        /// <summary>
        /// Disposes the test context.
        /// </summary>
        public void Dispose()
        {
        }

    }

}
