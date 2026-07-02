namespace OnlinePoll.Application.DTO
{
    public class QuestionAnswersDto
    {
        public string PollTitle { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
        public List<AnswerDto> Answers { get; set; } = new();
    }
}
