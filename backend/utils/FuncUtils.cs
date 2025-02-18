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

    }
}
