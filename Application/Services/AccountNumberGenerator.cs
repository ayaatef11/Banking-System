using System.Security.Cryptography;
using Application.Interfaces;

namespace Application.Services;

public class AccountNumberGenerator : IAccountNumberGenerator
{
    private const int AccountNumberLength = 10; 
    public string Generate()
    {
        return string.Create<object?>(AccountNumberLength, null, (span, _) =>
        {
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = (char)(RandomNumberGenerator.GetInt32(0, 10) + '0');
            }
        });
    }
}
