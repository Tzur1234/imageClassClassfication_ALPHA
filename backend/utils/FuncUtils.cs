using System;
using System.Linq;

namespace Company.Function
{
    public static class FuncUtils
    {
        // Method to validate the EventType is 'Microsoft.Storage.BlobCreated'
        public static bool IsBlobCreatedEventType(string eventType)
        {
            return string.Equals(eventType, "Microsoft.Storage.BlobCreated", StringComparison.OrdinalIgnoreCase);
        }

         public static string ExtractFileNameFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            try
            {
                Uri uri = new Uri(url);
                return System.IO.Path.GetFileName(uri.LocalPath); // Extracts the last segment (file name)
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to extract file name from URL: {url}", ex);
            }
        }

    }
}
