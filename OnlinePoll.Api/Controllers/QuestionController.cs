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
        [HttpPost("CreateQuestion/{id}")]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto) 
        {
            if (string.IsNullOrWhiteSpace(questionDto.QuestionText))
            {
                return BadRequest("QuestionText is required");
            }
          
            await _questionService.AddQuestionAsync(questionDto);
            return Ok();
        }

        [HttpGet("GetQuestion/{id}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            var res = await _questionService.GetQuestionAsync(questionId);
            if(string.IsNullOrEmpty(res.QuestionText))
            {
                return NotFound("Doesn't exist");
            }
            return Ok(res);
        }
        [HttpPut("UpdateQuestion/{id}")]
        public async Task<IActionResult> UpdateQuestion(int questionId, string questionText)
        {
            await _questionService.UpdateQuestion(questionId, questionText);
            return Ok();
        }
        [HttpDelete("DeleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            await _questionService.DeletQuestionAsync(questionId);
            return Ok();
        }
        [HttpGet("GetAllPollQuestions/{id}")]
        public async Task<IActionResult> GetAllPollQuestions(int pollId)
        {
            var questions = await _questionService.GetAllPollQuestions(pollId);
            return Ok(questions);
        }
    }
}
