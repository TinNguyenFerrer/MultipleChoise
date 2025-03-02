using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultipleChoise.Server.Service.Interface;

namespace MultipleChoise.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private readonly ICrudService<T> _service;

        protected BaseController(ICrudService<T> service)
        {
            _service = service;
        }

        [ActionName(nameof(GetById))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [ActionName(nameof(GetAll))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [ActionName(nameof(Create))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T entity)
        {
            await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity?.GetType().GetProperty("Id")?.GetValue(entity) }, entity);
        }

        [ActionName(nameof(Update))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] T entity)
        {
            var entityId = entity?.GetType().GetProperty("Id")?.GetValue(entity);
            if (entityId == null || !Guid.Equals(id, entityId))
            {
                return BadRequest();
            }

            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [ActionName(nameof(Delete))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
