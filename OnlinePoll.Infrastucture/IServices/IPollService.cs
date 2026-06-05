using OnlinePoll.Domain.Entities;

namespace OnlinePoll.Application.IService
{
    public interface IPollService
    {
        public Task AddQuestionAsync(Question question);
    }
}
