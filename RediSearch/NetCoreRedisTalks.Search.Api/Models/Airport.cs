namespace NetCoreRedisTalks.Search.Api.Models
{
    public class Airport
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }

        #region Cache Attributes
        public double Score { get; set; } = 0;
        public string Tag { get; set; }

        #endregion
    }
}