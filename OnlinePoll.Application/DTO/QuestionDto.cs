using OnlinePoll.Domain.Enums;

namespace OnlinePoll.Application.DTO
{
    public class QuestionDto
    {
        public string QuestionText { get; set; } = string.Empty;
        public int PollId { get; set; }
        public QuestionType Type { get; set; }
        public List<QuestionOptionDto> Options { get; set; } = new();
    }
}
