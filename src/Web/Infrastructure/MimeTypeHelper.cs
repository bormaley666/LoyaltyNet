using System;
using Microsoft.AspNetCore.StaticFiles;

namespace LoyaltyNet.Web.Infrastructure;

public static class MimeTypeHelper
{
    private const string DefaultMimeType = "application/octet-stream";

    public static string GetMimeType(string fileName)
    {
        ArgumentNullException.ThrowIfNull(fileName);

        if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var mimeType))
        {
            mimeType = DefaultMimeType;
        }

        return mimeType;
    }
}
