namespace OnlinePoll.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string? AnswerText { get; set; } = string.Empty;

        public int? QuestionOptionId { get; set; }
        public QuestionOption? QuestionOption { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
