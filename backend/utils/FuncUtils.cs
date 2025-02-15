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

        // Method to validate if the contentType is an image type (e.g., image/png, image/jpeg)
        public static bool IsValidImageContentType(string contentType)
        {
            // Define a list of accepted image content types
            string[] validImageContentTypes = new string[]
            {
                "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp", "image/tiff"
            };

            // Check if the contentType is one of the valid image types
            return validImageContentTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase);
        }
    }
}
