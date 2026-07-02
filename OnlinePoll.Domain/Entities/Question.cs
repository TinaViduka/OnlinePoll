using OnlinePoll.Domain.Enums;

namespace OnlinePoll.Domain.Entities
{
   
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public int PollId { get; set; }
        public Poll Poll { get; set; }= null!;
        public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
