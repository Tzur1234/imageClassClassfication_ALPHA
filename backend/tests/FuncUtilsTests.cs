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

        [Theory]
        [InlineData("image/jpeg", true)] // Valid content type
        [InlineData("image/png", true)] // Valid content type
        [InlineData("image/gif", true)] // Valid content type
        [InlineData("image/webp", true)] // Valid content type
        [InlineData("image/tiff", true)] // Valid content type
        [InlineData("application/json", false)] // Invalid content type
        [InlineData("image/xyz", false)] // Unknown content type
        [InlineData("", false)] // Empty string
        [InlineData(null, false)] // Null input
        public void IsValidImageContentType_ShouldReturnExpectedResult(string contentType, bool expected)
        {
            // Act
            bool result = FuncUtils.IsValidImageContentType(contentType);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
