using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Service.Interface;

namespace MultipleChoise.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;
        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [ActionName(nameof(GetResult))]
        [HttpPost]
        public async Task<IActionResult> GetResult([FromBody] ResultDto resultDto)
        {
            return Ok(await _resultService.GetResult(resultDto));
        }

        [ActionName(nameof(GetAll))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _resultService.GetAllAsync();
            return Ok(entities);
        }

        [ActionName(nameof(GetByQuizId))]
        [HttpGet("quiz/{id}")]
        public async Task<IActionResult> GetByQuizId(Guid id)
        {
            var entities = await _resultService.GetByQuizId(id);
            return Ok(entities);
        }


    }
}
