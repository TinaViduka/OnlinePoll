using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlinePoll.Application.DTO;
using OnlinePoll.Application.IService;

namespace OnlinePoll.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IPollService _questionService;
        public QuestionController(IPollService questionService)
        {
            _questionService = questionService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto) 
        {
            if (string.IsNullOrWhiteSpace(questionDto.QuestionText))
            {
                return BadRequest("QuestionText is required");
            }
          
            await _questionService.AddQuestionAsync(questionDto);
            return Ok();
        }
    }
}
