using OnlinePoll.Application.DTO;
using OnlinePoll.Application.DTO.Mapper;
using OnlinePoll.Application.IService;
using OnlinePoll.Domain.Entities;
using OnlinePoll.Infrastucture.Persistance;

namespace OnlinePoll.Infrastucture.Service
{
    public class PollService : IPollService
    {
        private readonly OnlinePollContext _context;
        public PollService(OnlinePollContext context)
        {
            _context = context;
        }

        public async Task AddPollAsync(PollDto pollDto)
        {
            Poll poll = PollMapper.ToDomain(pollDto);
            await _context.AddAsync(poll);
            await _context.SaveChangesAsync();
        }

        public async Task AddQuestionAsync(QuestionDto questionDto)
        {
            if(questionDto.PollId <=0)
            {
                throw new ArgumentException("PollId is incorrect");
            }

            Poll? poll = await _context.Polls.FindAsync(questionDto.PollId);
            if(poll == null)
            {
                throw new KeyNotFoundException($"Poll with ID  {questionDto.PollId} doesn't exist");
            }

            Question question = QuestionMapper.ToDomain(questionDto);
            await _context.AddAsync(questionDto);
            await _context.SaveChangesAsync();
        }
    }
}
