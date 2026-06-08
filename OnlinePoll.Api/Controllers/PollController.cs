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
        [HttpPost]
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
    }
}
