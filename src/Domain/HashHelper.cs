using System;
using System.Security.Cryptography;
using System.Text;

namespace LoyaltyNet.Domain;

public static class HashHelper
{
    public static string Hash(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));

        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    public static long HashLong(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        long result = 0;

        for (var i = 0; i < hash.Length; i++)
        {
            result ^= ((long) hash[i] << ((i % 8) * 8));
        }

        return result;
    }
}
