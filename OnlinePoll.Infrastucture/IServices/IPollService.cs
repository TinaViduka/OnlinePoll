using OnlinePoll.Application.DTO;
using OnlinePoll.Domain.Entities;

namespace OnlinePoll.Application.IService
{
    public interface IPollService
    {
        public Task AddQuestionAsync(QuestionDto questionDto);
        public Task AddPollAsync(PollDto pollDto);
    }
}
