using OnlinePoll.Application.IService;
using OnlinePoll.Domain.Entities;
using OnlinePoll.Infrastucture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePoll.Infrastucture.Service
{
    internal class PollService : IPollService
    {
        private readonly OnlinePollContext _context;
        public PollService(OnlinePollContext context)
        {
            _context = context;
        }
        public async Task AddQuestionAsync(Question question)
        {
            await _context.AddAsync(question);
            await _context.SaveChangesAsync();
        }
    }
}
