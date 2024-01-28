using AutoFixture;
using Frank.Security.Cryptography;
using Frank.Security.HaveIBeenPwned;
using Frank.Testing.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Frank.Security.Tests
{
    public class HaveIBeenPwnedClientTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IServiceProvider _serviceProvider;
        private readonly Fixture _fixture = new();
        
        public HaveIBeenPwnedClientTest(ITestOutputHelper output)
        {
            _output = output;

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();
            serviceCollection.AddTestLogging(output, LogLevel.Debug);
            serviceCollection.Configure<HibpConfiguration>(options =>
            {
                options.PwnedPasswordAddress = new Uri("https://api.pwnedpasswords.com/range");
            });
            serviceCollection.AddSingleton<IHaveIBeenPwnedClient, HaveIBeenPwnedClient>();
            
            _serviceProvider = serviceCollection.BuildServiceProvider(new ServiceProviderOptions() {ValidateOnBuild = true, ValidateScopes = true});
            
        }

        [Fact]
        public async Task CheckPassword_PwnedPassword_ReturnsTrue()
        {
            // Arrange
            var client = _serviceProvider.GetRequiredService<IHaveIBeenPwnedClient>();
            var password = "password";

            // Act
            var result = await client.IsPwnedAsync(password);

            // Assert
            Assert.True(result, "Should return True for pwned password");
            _output.WriteLine($"Password '{password}' is pwned");
        }

        [Fact]
        public async Task CheckPassword_ValidPassword_ReturnsFalse()
        {
            // Arrange
            var client = _serviceProvider.GetRequiredService<IHaveIBeenPwnedClient>();
            var password = new PasswordBuilder(16)
                .IncludeDigits()
                .IncludeLowercase()
                .IncludeUppercase()
                .IncludeSpecial()
                .Shuffle()
                .Build();
            // var password = _fixture.Create<string>();

            // Act
            var result = await client.IsPwnedAsync(password);

            // Assert
            _output.WriteLine($"Password '{password}' is not pwned");
            Assert.False(result, "Should return False for valid password");
        }

        [Fact]
        public async Task GetPasswordDetails_ReturnsListOfPasswords2()
        {
            // Arrange
            var client = _serviceProvider.GetRequiredService<IHaveIBeenPwnedClient>();
            var password = "password";

            // Act
            var result = await client.GetPasswordDetailsAsync(password);

            // Assert
            Assert.All(result, password => Assert.NotNull(password));
            _output.WriteLine($"Found {result.Count()} passwords");
            _output.WriteJson(result);
        }

        [Fact]
        public async Task GetPasswordDetails_ReturnsListOfPasswords()
        {
            // Arrange
            var client = _serviceProvider.GetRequiredService<IHaveIBeenPwnedClient>();
            var password = _fixture.Create<string>();

            // Act
            var result = await client.GetPasswordDetailsAsync(password);

            // Assert
            Assert.All(result, password => Assert.NotNull(password));
            _output.WriteLine($"Found {result.Count()} passwords");
            _output.WriteJson(result);
        }
    }
}