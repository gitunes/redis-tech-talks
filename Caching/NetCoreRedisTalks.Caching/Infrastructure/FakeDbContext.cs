namespace NetCoreRedisTalks.Caching.Infrastructure
{
    public static class FakeDbContext
    {
        public static List<Vehicle> Vehicles
        {
            get
            {
                return new List<Vehicle>
                {
                    new Vehicle
                    {
                        BrandId = 1,
                        BrandName = "Seat"
                    },
                    new Vehicle
                    {
                        BrandId = 2,
                        BrandName = "Volkswagen"
                    },
                    new Vehicle
                    {
                        BrandId = 3,
                        BrandName = "Audi"
                    }
                };
            }
        }
    }
}
