using OnlinePoll.Application.DTO;
using OnlinePoll.Domain.Entities;

namespace OnlinePoll.Application.IService
{
    public interface IPollService
    {
        public Task AddQuestionAsync(QuestionDto questionDto);
        public Task AddPollAsync(PollDto pollDto);
        public Task<QuestionDto> GetQuestionAsync(int questionId);
        public Task<PollDto> GetPollAsync(int pollId);
        public Task UpdatePoll(int pollId, string title, string description);
        public Task UpdateQuestion(int questionId, string questionText);
        public Task DeletePollAsync(int pollId);
        public Task DeletQuestionAsync(int questionId);
        public Task<List<QuestionDto>> GetAllPollQuestions(int pollId);
        public Task<List<PollDto>> GetAllPolls();
    }
}
