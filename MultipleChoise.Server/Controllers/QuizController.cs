using Microsoft.AspNetCore.Mvc;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Service;
using MultipleChoise.Server.Service.Interface;

namespace MultipleChoise.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly ICrudService<QuizDto> _service;
        private readonly IQuizService _quizService;

        public QuizController(ICrudService<QuizDto> service, IQuizService quizService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
        }

        [ActionName(nameof(GetById))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _quizService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([FromBody] QuizDto entity)
        {
            return Ok(await _quizService.CreateAndReturnAsync(entity));
        }

        [ActionName(nameof(Update))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] QuizDto entity)
        {
            var entityId = entity?.GetType().GetProperty("Id")?.GetValue(entity);
            if (entityId == null || !Guid.Equals(id, entityId))
            {
                return BadRequest();
            }

            return Ok(await _quizService.UpdateQuizAsync(entity));
        }

        [ActionName(nameof(Delete))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [ActionName(nameof(GetByNumber))]
        [HttpGet("by-number/{number}")]
        public async Task<IActionResult> GetByNumber([FromRoute] int number)
        {
            var entity = await _quizService.GetByNumberAsync(number);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
    }
}
