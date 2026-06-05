namespace OnlinePoll.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; } = string.Empty;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public int PollSubmissionId { get; set; }
        public PollSubmission PollSubmission { get; set; } = null!;
    }
}
