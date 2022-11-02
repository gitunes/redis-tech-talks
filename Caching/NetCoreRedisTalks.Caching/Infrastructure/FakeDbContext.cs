namespace NetCoreRedisTalks.Caching.Infrastructure
{
    public static class FakeDbContext
    {
        /// <summary>
        /// 6 adet meyve
        /// </summary>
        public static List<Fruit> Fruits
        {
            get
            {
                return new List<Fruit>
                {
                    new Fruit
                    {
                        FruitId = 1,
                        FruitName = "Elma",
                        Score = 10
                    },
                    new Fruit
                    {
                        FruitId = 2,
                        FruitName = "Portakal",
                        Score = 5
                    },
                    new Fruit
                    {
                        FruitId = 3,
                        FruitName = "Muz",
                        Score = 7
                    },
                    new Fruit
                    {
                        FruitId = 4,
                        FruitName = "Nar",
                        Score = 2
                    },
                    new Fruit
                    {
                        FruitId = 5,
                        FruitName = "Greyfurt",
                        Score = 1
                    },
                    new Fruit
                    {
                        FruitId = 6,
                        FruitName = "Kivi",
                        Score = 9
                    }
                };
            }
        }

        public static List<City> Cities
        {
            get
            {
                return new List<City>
                {
                    new City
                    {
                        Id = 34,
                        Name = "İstanbul"
                    },
                    new City
                    {
                        Id = 06,
                        Name = "Ankara"
                    },
                    new City
                    {
                        Id = 35,
                        Name = "İzmir"
                    },
                    new City
                    {
                        Id = 24,
                        Name = "Erzincan"
                    },
                };
            }
        }
    }
}
