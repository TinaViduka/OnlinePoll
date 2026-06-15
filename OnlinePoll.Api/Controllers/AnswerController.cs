using Microsoft.AspNetCore.Mvc;
using OnlinePoll.Application.DTO;
using OnlinePoll.Application.IService;

namespace OnlinePoll.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IPollService _answerService;
        public AnswerController (IPollService answerService)
        {
            _answerService = answerService;
        }
        [HttpPost("AddAnswer/{id}")]
        public async Task<IActionResult> AddAnswer([FromBody] AnswerDto answerDto)
        {
            if (string.IsNullOrEmpty(answerDto.AnswerText)) 
            {
                return BadRequest("AnswerText is required");
            }
            await _answerService.AddAnswerAsync(answerDto);
            return Ok();
        }
        [HttpGet("GetAnswer/{id}")]
        public async Task<IActionResult> GetAnswer(int answerId)
        {
            var res = await _answerService.GetAnswerAsync(answerId);
            if(string.IsNullOrEmpty(res.AnswerText))
            {
                return NotFound("Doesn't exist");
            }
            return Ok(res);
        }
        [HttpPut("UpdateAnswer/{id}")]
        public async Task<IActionResult> UpdateAnswer(int answerId, string answerText)
        {
            await _answerService.UpdateAnswerAsync(answerId, answerText);
            return Ok();
        }
        [HttpDelete("DeleteAnswer/{id}")]
        public async Task<IActionResult> DeleteAnswer(int answerId)
        {
            await _answerService.DeletAnswerAsync(answerId);
            return Ok();
        }
        [HttpGet("GetAllQuestionAnswers/{id}")]
        public async Task<IActionResult> GetAllQuestionAnswers(int questionId)
        {
            var anwsers = await _answerService.GetAllQuestionAnswers(questionId);
            return Ok(anwsers);
        }
    }
}
