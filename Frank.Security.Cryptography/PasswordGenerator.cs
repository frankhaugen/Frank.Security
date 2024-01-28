using System.Text;

namespace Frank.Security.Cryptography;

public class PasswordGenerator
{
    public string GeneratePassword(PasswordGeneratorOptions options)
    {
        var passwordBuilder = new PasswordBuilder(options.Length);
        if (options.IncludeUppercase)
            passwordBuilder.IncludeUppercase();
        if (options.IncludeLowercase)
            passwordBuilder.IncludeLowercase();
        if (options.IncludeDigits)
            passwordBuilder.IncludeDigits();
        if (options.IncludeSpecial)
            passwordBuilder.IncludeSpecial();
        if (options.IncludeHomoglyphs)
            passwordBuilder.IncludeHomoglyphs();
        if (options.IncludeWhitespace)
            passwordBuilder.IncludeWhitespace();
        passwordBuilder.Shuffle();
        return passwordBuilder.Build();
    }
}