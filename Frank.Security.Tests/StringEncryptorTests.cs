using Frank.Security.Cryptography;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;

namespace Frank.Security.Tests;

public class StringEncryptorTests(ITestOutputHelper output)
{
    [Fact]
    public void EncryptionDecryptionTest()
    {
        // Arrange
        var options = Options.Create(new StringEncryptorOptions());
        var stringEncryptor = new StringEncryptor(options);
        var original = "Hello, World!";
        var key = Guid.NewGuid();
        output.WriteLine($"Original string: {original}");
        output.WriteLine($"Key: {key}");

        // Act
        var encrypted = stringEncryptor.Encrypt(original, key);
        output.WriteLine($"Encrypted string: {encrypted}");

        var decrypted = stringEncryptor.Decrypt(encrypted, key);
        output.WriteLine($"Decrypted string: {decrypted}");

        // Assert
        Assert.Equal(original, decrypted);
    }
}