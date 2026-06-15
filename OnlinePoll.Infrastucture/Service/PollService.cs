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

            foreach (var poll in polls)
            {
                res.Add(PollMapper.ToDto(poll));
            }
            return res;
        }

        public async Task<List<QuestionDto>> GetAllPollQuestions(int pollId)
        {
            var question = await _context.Questions.Where(q => q.PollId == pollId).ToListAsync();

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

        public async Task AddAnswerAsync(AnswerDto answerDto)
        {
            if (answerDto.QuestionId <= 0)
                return;

            Question? question = await _context.Questions.FindAsync(answerDto.QuestionId);
            if (question == null) 
                return;

            if(answerDto.PollSubmissionId <= 0)
                return;

            PollSubmission? pollSubmission = await _context.PollSubmissions.FindAsync(answerDto.PollSubmissionId);
            if (pollSubmission == null)
                return;

            Answer answer = AnswerMapper.ToDomain(answerDto);
            await _context.AddAsync(answer);
            await _context.SaveChangesAsync();

        }

        public async Task AddPollSubmissionAsync(PollSubmissionDto pollSubmissionDto)
        {
            if (pollSubmissionDto.PollId <= 0)
                return;


            Poll? poll = await _context.Polls.FindAsync(pollSubmissionDto.PollId);
            if (poll == null)
                return;

            PollSubmission? pollSubmission = PollSubmissionMapper.ToDomain(pollSubmissionDto);  
            await _context.AddAsync(pollSubmission);
            await _context.SaveChangesAsync();
        }

        public async Task<AnswerDto> GetAnswerAsync(int answerId)
        {
            var res = await _context.Answers.FirstOrDefaultAsync(a => a.Id == answerId);
            if(res == null)
                return new AnswerDto();

            AnswerDto answerDto = AnswerMapper.ToDto(res);
            return answerDto;
        }

        public async Task UpdateAnswerAsync(int answerId, string answerText)
        {
            var res = await _context.Answers.FirstOrDefaultAsync(a => a.Id == answerId);
            res.AnswerText = answerText;
            _context.Update(res);
            await _context.SaveChangesAsync();
        }

        public async Task DeletAnswerAsync(int answerId)
        {
            var res = await _context.Answers.FirstOrDefaultAsync(q => q.Id == answerId);
            if (res == null)
                return;
            _context.Remove(res);
            await _context.SaveChangesAsync();
            return;
        }

        public async  Task <List<AnswerDto>> GetAllQuestionAnswers(int questionId)
        {
            var answers = await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();

            List<AnswerDto> res = new();

            foreach (var answer in answers) 
            {
                res.Add(AnswerMapper.ToDto(answer));
            }
            return res;
        }

        public async Task<PollSubmissionDto> GetPollSubmissionAsync(int pollSubmissionId)
        {
            var res = await _context.PollSubmissions.FirstOrDefaultAsync(p => p.Id == pollSubmissionId);
            if (res == null)
                return new PollSubmissionDto();

            PollSubmissionDto pollSubmissionDto = PollSubmissionMapper.ToDto(res);
            return pollSubmissionDto;
        }

        public async Task DeletPollSubmissionAsync(int pollSubmissioId)
        {
            var res = await _context.PollSubmissions.FirstOrDefaultAsync(q => q.Id == pollSubmissioId);
            if (res == null)
                return;
            _context.Remove(res);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<PollSubmissionDto>> GetAllPollSubmissions()
        {
            var pollSubmissions = await _context.PollSubmissions.ToListAsync();

            List<PollSubmissionDto> res = new();

            foreach (var pollSubmission in pollSubmissions)
            {
                res.Add(PollSubmissionMapper.ToDto(pollSubmission));
            }
            return res;
        }
    }
}
