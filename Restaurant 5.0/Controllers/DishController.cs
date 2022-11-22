using Microsoft.AspNetCore.Mvc;
using Restaurant_5._0.Models;
using Restaurant_5._0.Services;
using System.Collections.Generic;

namespace Restaurant_5._0.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }
        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
        {
            int result = _service.Create(restaurantId, dto);
            return Created($"api/restaurant/{restaurantId}/dish/{result}", null);
        }
        [HttpGet]
        public ActionResult<List<DishDto>> Get([FromRoute] int restaurantId)
        {
            List<DishDto> result = _service.GetAll(restaurantId);
            return Ok(result);
        }
        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            DishDto result = _service.GetById(restaurantId, dishId);
            return Ok(result);
        }
        [HttpDelete("{dishId}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            _service.Delete(restaurantId, dishId);
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _service.Delete(restaurantId);
            return NoContent();
        }
    }
}
