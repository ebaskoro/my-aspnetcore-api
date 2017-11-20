using Xunit;
using Api.Services;


namespace Api.Tests.Services
{

    public class MessagingOptionsTest
    {
        
        [Fact]
        public void Constructor_Initialises_Correctly()
        {
            var target = new MessagingOptions();

            Assert.Null(target.Url);
            Assert.False(target.HasUrl);
        }
        
    }

}
