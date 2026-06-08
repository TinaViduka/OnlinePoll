using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlinePoll.Application.DTO;
using OnlinePoll.Application.DTO.Mapper;
using OnlinePoll.Application.IService;
using OnlinePoll.Infrastucture.Service;

namespace OnlinePoll.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly IPollService _pollService;
        public PollController (IPollService pollService) 
        {
            _pollService = pollService;
        }
        [HttpPost("CreatePoll/{id}")]
        public async Task<IActionResult> CreatePoll([FromBody] PollDto pollDto)
        {
            if (string.IsNullOrWhiteSpace(pollDto.Title))
            {
                return BadRequest("Title is required");
            }
            pollDto.Created = DateTime.Now;
            await _pollService.AddPollAsync(pollDto);
            return Ok();  
        }
        [HttpGet("GetPoll/{id}")]
        public async Task<IActionResult> GetPoll(int pollId)
        {
            var res = await _pollService.GetPollAsync(pollId);
            if(string.IsNullOrEmpty(res.Title))
            {
                return NotFound("Poll doesn't exist in database");
            }
            return Ok(res);
        }
        [HttpPut("UpdatePoll/{id}")]
        public async Task<IActionResult> UpdatePoll(int pollId, string title, string description)
        {
            await _pollService.UpdatePoll(pollId, title, description);
            return Ok();
        }
        [HttpDelete("DeletePoll/{id}")]
        public async Task<IActionResult> DeletePoll(int pollId)
        {
            await _pollService.DeletePollAsync(pollId);
            return Ok();    
        }
        [HttpGet("GetAllPolls/{id}")]
        public async Task<IActionResult> GetAllPolls()
        {
            var polls = await _pollService.GetAllPolls();
            return Ok(polls);
        }  
    }
}
