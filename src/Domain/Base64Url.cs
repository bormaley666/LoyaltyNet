using System;
using System.Text;
using System.Text.Json;
using NeoSmart.Utils;

namespace LoyaltyNet.Domain;

public static class Base64Url
{
    public static string Encode<T>(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var json = JsonSerializer.Serialize(value);

        return UrlBase64.Encode(Encoding.UTF8.GetBytes(json));
    }

    public static T Decode<T>(string encoded)
    {
        ArgumentNullException.ThrowIfNull(encoded);

        var json = Encoding.UTF8.GetString(UrlBase64.Decode(encoded));

        return JsonSerializer.Deserialize<T>(json) ?? throw new InvalidOperationException();
    }
}
