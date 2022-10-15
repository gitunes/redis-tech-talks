﻿namespace NetCoreRedisTalks.Search.Api.Services
{
    public class AirportService : IAirportService
    {
        private readonly Client _client;

        public AirportService(IConnectionMultiplexer connectionMultiplexer)
        {
            string indexName = "airport-idx";
            IDatabase database = connectionMultiplexer.GetDatabase(0);  //0 haricinde bir db kullanılamıyor.

            _client = new Client(indexName, database);
        }

        public async Task<IEnumerable<Airport>> SearchAsync(string word)
        {
            Query q = new Query($"(@code:{word})|(@city:{word}*)|(@Tag:{{{word}}})")
               .SetLanguage("Turkish");

            SearchResult searchResult = await _client.SearchAsync(q);
            if (searchResult.TotalResults == 0)
                return default;

            return searchResult.Documents.ToAirports();
        }

        public async Task<Airport> GetAsync(string docId)
        {
            Document document = await _client.GetDocumentAsync(docId);

            string jsonAirportItem = JsonConvert.SerializeObject(document.GetProperties());
            Airport airport = JsonConvert.DeserializeObject<Airport>(jsonAirportItem);

            return airport;
        }

        public async Task<bool> AddAsync(string docId, Airport airport)
        {
            var documentDictionary = airport.ToEntityDictionary();

            dynamic score = documentDictionary.FirstOrDefault(x => x.Key == "Score").Value ?? 1;

            Dictionary<string, RedisValue> dictDocRedis = new();

            foreach (var item in documentDictionary)
            {
                dictDocRedis.Add(item.Key, item.Value ?? string.Empty);
            }

            return await _client.AddDocumentAsync(docId, dictDocRedis, score);
        }

        public async Task<bool> UpdateAsync(string docId, Airport airport)
        {
            var documentDictionary = airport.ToEntityDictionary();

            dynamic score = documentDictionary.FirstOrDefault(x => x.Key == "Score").Value ?? 1;

            Dictionary<string, RedisValue> dictDocRedis = new();

            foreach (var item in documentDictionary)
            {
                dictDocRedis.Add(item.Key, item.Value ?? string.Empty);
            }

            return await _client.UpdateDocumentAsync(docId, dictDocRedis, score);
        }

        public async Task<bool> DeleteAsync(string docId)
        {
            return await _client.DeleteDocumentAsync(docId);
        }

        public async Task<bool> CreateIndexAsync()
        {
            Schema schema = new();
            schema.AddTextField("Code");
            schema.AddTextField("Name");
            schema.AddTextField("City", 5);
            schema.AddTextField("State");
            schema.AddTextField("Country");
            schema.AddTagField("Tag");
            schema.AddGeoField("GeoPoint");

            return await _client.CreateIndexAsync(schema, new Client.ConfiguredIndexOptions(Client.IndexOptions.Default));
        }

        public async Task<bool> DropIndexAsync()
        {
            return await _client.DropIndexAsync();
        }

        public async Task PushSampleDataAsync()
        {
            using StreamReader streamReader = new("airports.json");
            string airportsJsonData = await streamReader.ReadToEndAsync();
            List<Airport> airports = JsonConvert.DeserializeObject<List<Airport>>(airportsJsonData);

            foreach (var airport in airports)
            {
                var fields = new Dictionary<string, RedisValue>
                {
                    { "Code", airport.Code },
                    { "Name", airport.Name },
                    { "City", airport.City },
                    { "State", airport.State },
                    { "Country", airport.Country },
                    { "Tag", airport.Tag },
                    { "Lat", airport.Lat },
                    { "Lon", airport.Lon },
                    { "GeoPoint", $"{ airport.Lon },{ airport.Lat }" }
                };

                await _client.AddDocumentAsync(Guid.NewGuid().ToString(), fields);
            }
        }
    }
}
