using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog.LayoutRenderers;
using Restaurant_5._0.Entities;
using Restaurant_5._0.Exceptions;
using Restaurant_5._0.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant_5._0.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        List<DishDto> GetAll(int restuarantId);
        DishDto GetById(int restaurantId, int dishId);
        void Delete(int restaurantId, int dishId);
        void Delete(int restaurantId);
    }

public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        
        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dto)
        {
            CheckRestaurant(restaurantId);
            Dish result = _mapper.Map<Dish>(dto);
            result.RestaurantId = restaurantId;
            _context.Dishes.Add(result);
            _context.SaveChanges();

            return result.Id;
        }

        public void Delete(int restaurantId, int dishId)
        {
            CheckRestaurant(restaurantId);
            Dish dish = CheckDish(restaurantId, dishId);
            _context.Dishes.Remove(dish);
            _context.SaveChanges();
        }

        public void Delete(int restaurantId)
        {
            Restaurant restaurant = GetRestaurantById(restaurantId);
            _context.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            Restaurant result = _context
                .Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == restaurantId);
            if(result == null)
            {
                throw new NotFoundException("Restaraunt not found");
            }
            return _mapper.Map<List<DishDto>>(result.Dishes);
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            CheckRestaurant(restaurantId);
            Dish dish = CheckDish(restaurantId, dishId);
            return _mapper.Map<DishDto>(dish);
        }
        private void CheckRestaurant(int restaurantId)
        {
            Restaurant restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException("Restaraunt not found");
            }
        }
        private Dish CheckDish(int restaurantId, int dishId)
        {
            Dish dish = _context.Dishes.FirstOrDefault(x => x.Id == dishId);
            if (dish == null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }
            return dish;
        }
        private Restaurant GetRestaurantById(int restaurantId)
        {
            CheckRestaurant(restaurantId);
            Restaurant restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
            return restaurant;
        }
    }
}
