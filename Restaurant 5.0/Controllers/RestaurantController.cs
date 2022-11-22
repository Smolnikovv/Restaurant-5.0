using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_5._0.Entities;
using Restaurant_5._0.Models;
using Restaurant_5._0.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Restaurant_5._0.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    //[Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;
        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Policy = "HasNationality")]
        public ActionResult<List<RestaurantDto>> Get([FromQuery] RestaurantQuery query)
        {
            var result = _service.GetAll(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetId([FromRoute] int id)
        {
            RestaurantDto result = _service.GetById(id);        
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        public ActionResult Post([FromBody] CreateRestaurantDto dto)
        {
            var userId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            int id = _service.Create(dto);
            return Created($"/api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            _service.Delete(id);
            return NoContent();            
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            _service.Put(dto, id);
            return NoContent();
        }
    }
}
