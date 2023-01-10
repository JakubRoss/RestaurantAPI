using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder 
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }

        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name= "Koseba",
                    Description = "Pyszny, najostrzejszy sos do jakze miernego kebsa",
                    ContactEmail="koskeba@kebakos.pl",
                    ContactNumber = "725 195 390",
                    HasDelivery= true,
                    Dishes = new List<Dish>
                    {
                        new Dish()
                        {
                            Name="UberKeb",
                            Price = 15.50M
                        },
                        new Dish()
                        {
                            Name = "KingBurg",
                            Price = 9.9M
                        }
                    },
                    Adress = new Adress()
                    {
                        City = "Wiecbork",
                        Street = "Gdanska 18",
                        PostalCode = "89-410",
                    }
                },
                new Restaurant()
                {
                    Name= "KFC",
                    Description = "Kentucky Fried Chicken",
                    ContactEmail="kontakt@amrest.eu",
                    ContactNumber = "801 46 46 46.",
                    HasDelivery= true,
                    Dishes = new List<Dish>
                    {
                        new Dish()
                        {
                            Name="Grander Big Box",
                            Description = "Kanapka Grander, 5 Hot Wings (pikantne skrzydelka), duze frytki 115g, Wielka Dolewka",
                            Price = 35.99M
                        },
                        new Dish()
                        {
                            Name = "QURRITO GRANDE",
                            Description ="Qurrito w wersji Grande! Soczyste i pikantne Bites w towarzystwie wyrazistego, " +
                            "ciagnacego sie sera Cheddar, polane sosem BBQ, zapieczone w delikatnej pszennej tortilli.",
                            Price = 21.99M
                        }
                    },
                    Adress = new Adress()
                    {
                        City = "Bydgoszcz",
                        Street = "Aleja Jana Pawla II 123",
                        PostalCode = "85-140",
                    }
                }
            };
            return restaurants;
        }
    }
}