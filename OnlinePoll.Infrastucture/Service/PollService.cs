using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlinePoll.Application.DTO;
using OnlinePoll.Application.DTO.Mapper;
using OnlinePoll.Application.IService;
using OnlinePoll.Domain.Entities;
using OnlinePoll.Domain.Enums;
using OnlinePoll.Infrastucture.Persistance;
using System.ComponentModel.DataAnnotations;

namespace OnlinePoll.Infrastucture.Service 
{
    public class PollService : IPollService
    {
        private readonly OnlinePollContext _context;
        private readonly ILogger<PollService> _logger;
        public PollService(OnlinePollContext context, ILogger<PollService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddPollAsync(PollDto pollDto)
        {
            Poll poll = PollMapper.ToDomain(pollDto);
            await _context.AddAsync(poll);
            await _context.SaveChangesAsync();
        }

        public async Task AddQuestionAsync(QuestionDto questionDto)
        {
            if (questionDto.PollId <= 0)
            {
                _logger.LogWarning("Poll with that ID doesn't exist");
                return;
            }

            Poll? poll = await _context.Polls.FindAsync(questionDto.PollId);
            if (poll is null)
            {
                _logger.LogWarning("The poll doesn't exist");
                return;
            }

            Question question = QuestionMapper.ToDomain(questionDto);
            await _context.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePollAsync(int pollId)
        {
            var rez = await _context.Polls.FirstOrDefaultAsync(p => p.Id == pollId);
            if (rez is null)
            {
                _logger.LogWarning("Poll with ID {PollID} doesn't exist", pollId);
                return;
            }

            bool hasQuestions = await _context.Questions.AnyAsync(q => q.PollId == pollId);

            if (hasQuestions)
            {
                _logger.LogWarning("Poll with ID {PollID} has question", pollId);
                return;
            }

            _context.Polls.Remove(rez);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task DeletQuestionAsync(int questionId)
        {
            var res = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
            if (res is null)
            {
                _logger.LogWarning("Question with ID {QuestionID} doesn't exist", questionId);
                return;
            }

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
            var question = await _context.Questions.Include(q => q.Options).Where(q => q.PollId == pollId).ToListAsync();

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
            if (rez is null)
            {
                _logger.LogWarning("Poll with ID {PollID} doesn't exist", pollId);
                return new PollDto();
            }

            PollDto pollDto = PollMapper.ToDto(rez);
            return pollDto;
        }

        public async Task<QuestionDto> GetQuestionAsync(int questionId, int pollId)
        {
            var rez = await _context.Questions.Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == questionId && q.PollId == pollId);
            if (rez is null)
            {
                _logger.LogWarning("Question with ID {QuestionID} doesn't exist in that poll", questionId);
                return new QuestionDto();
            }


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
            res.QuestionText = questionText;
            _context.Update(res);
            await _context.SaveChangesAsync();
        }

        public async Task AddAnswerAsync(AnswerDto answerDto)
        {
            if (answerDto.QuestionId <= 0)
            {
                _logger.LogWarning("Question with that ID doesn't exist");
                return;
            }


            Question? question = await _context.Questions.FindAsync(answerDto.QuestionId);
            if (question is null)
            {
                _logger.LogWarning("Question with ID {QuestionID} doesn't exist", answerDto.QuestionId);
                return;
            }


            Answer answer = AnswerMapper.ToDomain(answerDto);
            await _context.AddAsync(answer);
            await _context.SaveChangesAsync();

        }

        public async Task<AnswerDto> GetAnswerAsync(int answerId, int questionId)
        {
            var res = await _context.Answers.FirstOrDefaultAsync(a => a.Id == answerId && a.QuestionId == questionId);
            if (res is null)
            {
                _logger.LogWarning("Answer with ID {AnswerID} doesn't exist for the question with ID {QuestionID}", answerId, questionId);
                return new AnswerDto();
            }

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
            if (res is null)
            {
                _logger.LogWarning("Answer with ID {AnswerID} doesn't exist", answerId);
                return;
            }

            _context.Remove(res);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<AnswerDto>> GetAllQuestionAnswers(int questionId)
        {
            var answers = await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();

            List<AnswerDto> res = new();

            foreach (var answer in answers)
            {
                res.Add(AnswerMapper.ToDto(answer));
            }
            return res;
        }

        public async Task<List<QuestionDto>> GetQuestionsByType(QuestionType questionType)
        {
            var questions = await _context.Questions
                .Where(q => q.Type == questionType)
                .ToListAsync();

            List<QuestionDto> res = new();

            foreach (var question in questions)
            {
                res.Add(QuestionMapper.ToDto(question));
            }
            return res;
        }

        public async Task<QuestionAnswersDto> GetAllAnswersForQuestion(int questionId)
        {
            var answers = await _context.Questions
                .Where(q => q.Id == questionId)
                .Select(q => new QuestionAnswersDto 
                { 
                    PollTitle = q.Poll.Title,
                    QuestionText = q.QuestionText,
                    Answers = _context.Answers
                        .Where(a => a.QuestionId == q.Id)
                        .Select(a => new AnswerDto
                        {
                            AnswerText = a.AnswerText,
                            QuestionId = a.QuestionId,
                            QuestionOptionId = a.QuestionOptionId
                        }).ToList()
                }).FirstOrDefaultAsync();

            return answers;
        }
    }
}
