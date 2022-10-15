namespace NetCoreRedisTalks.Search.Api.Utilities
{
    public static class RediSearchExtensions
    {
        public static Dictionary<string, dynamic> ToEntityDictionary<T>(this T data)
        {
            return data.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(data, null));
        }

        public static List<Airport> ToAirports(this List<Document> docList)
        {
            List<Airport> newDoc = new();

            foreach (var item in docList)
            {
                var jsonRedisItem = JsonConvert.SerializeObject(item.GetProperties());
                JObject jItemObj = JObject.Parse(jsonRedisItem);
                jItemObj["Id"] = item.Id;
                Airport castJsonObj = jItemObj.ToObject<Airport>();

                newDoc.Add(castJsonObj);
            }

            return newDoc;
        }
    }
}
