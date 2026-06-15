using Microsoft.AspNetCore.Mvc;
using OnlinePoll.Application.DTO;
using OnlinePoll.Application.IService;

namespace OnlinePoll.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollSubmissionController : ControllerBase
    {
        private readonly IPollService _pollSubmissionService;
        public PollSubmissionController(IPollService pollSubmissionService)
        {
            _pollSubmissionService = pollSubmissionService;
        }
        [HttpPost("AddPollSubmission/{id}")]
        public async Task<IActionResult> AddPollSubmission([FromBody] PollSubmissionDto pollSubmissionDto)
        {
            await _pollSubmissionService.AddPollSubmissionAsync(pollSubmissionDto);
            return Ok();
        }
        [HttpGet("GetPollSubmission/{id}")]
        public async Task<IActionResult> GetPollSubmission(int pollSubmissionId)
        {
            var pollSubmission = await _pollSubmissionService.GetPollSubmissionAsync(pollSubmissionId);

            if (pollSubmission == null)
                return NotFound("Doesn't exist");

            return Ok(pollSubmission);
        }
        [HttpDelete("DeletePollSubmission/{id}")]
        public async Task<IActionResult> DeletePollSubmission(int pollsubmissionId)
        {
            await _pollSubmissionService.DeletPollSubmissionAsync(pollsubmissionId);
            return Ok();
        }
        [HttpGet("GetAllPollSubmissions/{id}")]
        public async Task<IActionResult> GetAllPollSubmisssions()
        {
            var pollSubmission = await _pollSubmissionService.GetAllPollSubmissions();
            return Ok(pollSubmission);
        }
    }
}
