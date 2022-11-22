using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant_5._0.Authorization;
using Restaurant_5._0.Entities;
using Restaurant_5._0.Exceptions;
using Restaurant_5._0.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Restaurant_5._0.Services
{
    public interface IRestaurantService
    {
        RestaurantDto GetById(int id);
        PageResult<RestaurantDto> GetAll(RestaurantQuery query);
        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        void Put(UpdateRestaurantDto dto, int id);
        

    }
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        public IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContext;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService, IUserContextService userContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContext = userContext;
        }
        public RestaurantDto GetById(int id)
        {
            Restaurant result = _dbContext
               .Restaurants
               .Include(x => x.Address)
               .Include(x => x.Dishes)
               .FirstOrDefault(x => x.Id == id);
            if(result == null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            return _mapper.Map<RestaurantDto>(result);
        }
        public PageResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            var baseQuery = _dbContext
                .Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .Where(x => query.SearchPhrase == null || (x.Name.ToLower().Contains(query.SearchPhrase.ToLower()) || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), x=>x.Name },
                    {nameof(Restaurant.Description), x=>x.Description },
                    {nameof(Restaurant.Category), x=>x.Category }
                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.Ascending ?
                    baseQuery.OrderBy(x => x.Name)
                    : baseQuery.OrderByDescending(x => x.Name);
            }

            var restaurants = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var itemsCount = baseQuery.Count();
            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            var result = new PageResult<RestaurantDto>(restaurantDtos, itemsCount, query.PageSize, query.PageNumber);

            return result;
        }
        public int Create(CreateRestaurantDto dto)
        {
            Restaurant result = _mapper.Map<Restaurant>(dto);
            result.CreatedById = _userContext.GetUserId;
            _dbContext.Restaurants.Add(result);
            _dbContext.SaveChanges();
            return result.Id;
        }
        public void Delete(int id)
        {
            _logger.LogWarning($"Restaurant with id: {id} DELETE action invoked");
            Restaurant result = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if(result == null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContext.User, result, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            _dbContext.Restaurants.Remove(result);
            _dbContext.SaveChanges();
            
        }
        public void Put(UpdateRestaurantDto dto, int id)
        {
            Restaurant result = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContext.User, result, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            result.Name = dto.Name;
            result.Description = dto.Description;
            result.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();
        }
    }
}
