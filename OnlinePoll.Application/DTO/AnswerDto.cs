namespace OnlinePoll.Application.DTO
{
    public class AnswerDto
    {
        public string AnswerText { get; set; } = string.Empty;

        public int QuestionId { get; set; }

        public int PollSubmissionId { get; set; }
    }
}
