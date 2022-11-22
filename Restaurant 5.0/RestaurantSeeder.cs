using Restaurant_5._0.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant_5._0
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
                if(!_dbContext.Roles.Any())
                {
                    List<Role> roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Restaurants.Any())
                {
                    List<Restaurant> restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
        private List <Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "User"
                }
            };
            return roles;
        }
        private List <Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "FastFood",
                    Description = "Cos",
                    ContactEmail = "sad",
                    ContactNumber = "123asd",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Jeden",
                            Price = 10.30m
                        },
                        new Dish()
                        {
                            Name = "Dwa",
                            Price = 5.30m
                        }
                    },
                    Address = new Address()
                    {
                        City = "Krakow",
                        Street = "Dluga",
                        PostalCode = "31-450"
                    }
                },
                new Restaurant()
                {
                    Name = "KFC2",
                    Category = "FastFood2",
                    Description = "Cos2",
                    ContactEmail = "sad2",
                    ContactNumber = "123asd2",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Jeden2",
                            Price = 10.30m
                        },
                        new Dish()
                        {
                            Name = "Dwa2",
                            Price = 5.30m
                        }
                    },
                    Address = new Address()
                    {
                        City = "Krakow2",
                        Street = "Dluga2",
                        PostalCode = "31-4502"
                    }
                }
            };
            return restaurants;
        }
        
    }
        
}
