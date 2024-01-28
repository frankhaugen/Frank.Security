using System.Diagnostics;
using FluentAssertions;
using Frank.Security.Cryptography;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;

namespace Frank.Security.Tests;

public class PasswordHasherTests(ITestOutputHelper outputHelper)
{
    [Theory]
    [InlineData("password", 128)]
    [InlineData("password", 256)]
    [InlineData("password", 512)]
    [InlineData("password", 1024)]
    [InlineData("password", 2048)]
    [InlineData("password", 4096)]
    [InlineData("password", 8192)]
    [InlineData("password", 16384)]
    [InlineData("password", 32768)]
    [InlineData("password", 65536)]
    public void HashPassword(string password, int iterations)
    {
        // Arrange
        var options = Options.Create(new PasswordHasherOptions { Iterations = iterations });
        var hasher = new PasswordHasher(options);
        
        // Act
        var stopwatch = Stopwatch.StartNew();
        var hash = hasher.HashPassword(password);
        stopwatch.Stop();
        outputHelper.WriteLine(hash);
        
        var result = hasher.CompareHashedPassword(hash, password);
        
        // Assert
        hash.Should().NotBeNullOrEmpty();
        result.Should().BeTrue();
        outputHelper.WriteLine($"Hashing took {stopwatch.ElapsedMilliseconds} ms for {iterations} iterations.");
    }
}