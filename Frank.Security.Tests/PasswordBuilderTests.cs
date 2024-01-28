using FluentAssertions;
using Frank.Security.Cryptography;
using Xunit.Abstractions;

namespace Frank.Security.Tests;

public class PasswordBuilderTests
{
    private readonly ITestOutputHelper _output;

    public PasswordBuilderTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void GenerateFromSettings_ShouldCreatePassword()
    {
        // Arrange
        PasswordGeneratorOptions options = new();

        // Act
        var act = () => PasswordBuilder.GenerateFromSettings(options);

        // Assert
        act.Should().NotThrow();
        var password = act.Invoke();
        _output.WriteLine($"Generated Password: {password}");
        Assert.NotNull(password);
        Assert.Equal(options.Length, password.Length);
    }
}