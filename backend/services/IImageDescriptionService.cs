using System.Threading.Tasks;

namespace Company.Function
{
    // Define the interface for the ImageDescriptionService
    public interface IImageDescriptionService
    {
        // Define the contract for the method that analyzes an image
        Task<string> AnalyzeImageAsync(string imageUrl);
    }
}
