namespace Company.Function
{
    public interface ICosmosDbService
    {
        Task UpsertAnalysisResultAsync(string id, string analysisResult);
        Task<dynamic> GetAnalysisResultAsync(string filename);
    }
}
