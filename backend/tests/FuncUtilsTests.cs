using Xunit;
using Company.Function;

namespace Company.Function
{
    public class FuncUtilsTests
    {
        [Theory]
        [InlineData("Microsoft.Storage.BlobCreated", true)] // Valid event type
        [InlineData("microsoft.storage.blobcreated", true)] // Case insensitive check
        [InlineData("Microsoft.Storage.FileCreated", false)] // Invalid event type
        [InlineData("", false)] // Empty string
        [InlineData(null, false)] // Null input
        public void IsBlobCreatedEventType_ShouldReturnExpectedResult(string eventType, bool expected)
        {
            // Act
            bool result = FuncUtils.IsBlobCreatedEventType(eventType);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
