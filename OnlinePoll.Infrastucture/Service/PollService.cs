using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlinePoll.Application.DTO;
using OnlinePoll.Application.DTO.Mapper;
using OnlinePoll.Application.IService;
using OnlinePoll.Domain.Entities;
using OnlinePoll.Infrastucture.Persistance;
using System.ComponentModel.DataAnnotations;

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
                return;
           

            Poll? poll = await _context.Polls.FindAsync(questionDto.PollId);
            if(poll == null)
                return;

            Question question = QuestionMapper.ToDomain(questionDto);
            await _context.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePollAsync(int pollId)
        {
            var rez = await _context.Polls.FirstOrDefaultAsync(p => p.Id == pollId);
            if (rez == null)
                return;

            _context.Remove(rez);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task DeletQuestionAsync(int questionId)
        {
            var res = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);   
            if(res == null)
                return;
            _context.Remove(res);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<PollDto>> GetAllPolls()
        {
            var polls = await _context.Polls.ToListAsync();

            List<PollDto> res = new();

            foreach (var pol in polls)
            {
                res.Add(PollMapper.ToDto(pol));
            }
            return res;
        }

        public async Task<List<QuestionDto>> GetAllPollQuestions(int pollId)
        {
            var question = await _context.Questions.Where(p => p.Id == pollId).ToListAsync();

            List<QuestionDto> res = new();

            foreach (var questions in question)
            {
                res.Add(QuestionMapper.ToDto(questions));
            } 
            return res;
        }

        public async Task<PollDto> GetPollAsync(int pollId)
        {
            var rez = await _context.Polls.FirstOrDefaultAsync(p => p.Id == pollId);
            if (rez == null)
                return new PollDto();

            PollDto pollDto = PollMapper.ToDto(rez);
            return pollDto;
        }

        public async Task<QuestionDto> GetQuestionAsync(int questionId)
        {
            var rez = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
            if (rez == null)
                return new QuestionDto();

            QuestionDto questionDto = QuestionMapper.ToDto(rez);
            return questionDto;
        }

        public async Task UpdatePoll(int pollId, string title, string description)
        {
            var rez = await _context.Polls.FirstOrDefaultAsync(p => p.Id == pollId);
            rez.Title = title;
            rez.Description = description;
            _context.Update(rez);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuestion(int questionId, string questionText)
        {
            var res = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
            res.QuestionText= questionText;
            _context.Update(res);
            await _context.SaveChangesAsync();
        }
    }
}
