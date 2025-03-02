using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Service.Interface;

namespace MultipleChoise.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : BaseController<AnswerDto>
    {
        public AnswerController(ICrudService<AnswerDto> answerService) : base(answerService)
        {
        }
    }
}
