namespace NetCoreRedisTalks.Caching.Infrastructure
{
    public static class FakeDbContext
    {
        /// <summary>
        /// 6 adet araç markası
        /// </summary>
        public static List<Vehicle> Vehicles
        {
            get
            {
                return new List<Vehicle>
                {
                    new Vehicle
                    {
                        BrandId = 1,
                        BrandName = "Seat",
                        Score = 10
                    },
                    new Vehicle
                    {
                        BrandId = 2,
                        BrandName = "Volkswagen",
                        Score = 5
                    },
                    new Vehicle
                    {
                        BrandId = 3,
                        BrandName = "Audi",
                        Score = 7
                    },
                    new Vehicle
                    {
                        BrandId = 4,
                        BrandName = "Volkswagen Ticari",
                        Score = 2
                    },
                    new Vehicle
                    {
                        BrandId = 5,
                        BrandName = "Porsche",
                        Score = 1
                    },
                    new Vehicle
                    {
                        BrandId = 6,
                        BrandName = "Skoda",
                        Score = 9
                    }
                };
            }
        }
    }
}
