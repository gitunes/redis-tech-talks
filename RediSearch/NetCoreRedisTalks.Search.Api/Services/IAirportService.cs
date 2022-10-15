namespace NetCoreRedisTalks.Search.Api.Services
{
    public interface IAirportService
    {
        Task<IEnumerable<Airport>> SearchAsync(string word);
        Task<Airport> GetAsync(string docId);
        Task<bool> AddAsync(string docId, Airport airport);
        Task<bool> UpdateAsync(string docId, Airport airport);
        Task<bool> DeleteAsync(string docId);

        Task<bool> CreateIndexAsync();
        Task<bool> DropIndexAsync();

        Task PushSampleDataAsync();
    }
}
