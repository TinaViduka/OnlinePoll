namespace OnlinePoll.Domain.Entities
{
    public class QuestionOption
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
