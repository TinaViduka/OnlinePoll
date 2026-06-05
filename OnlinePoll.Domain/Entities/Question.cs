namespace OnlinePoll.Domain.Entities
{
   
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;

        public int PollId { get; set; }
        public Poll Poll { get; set; }= null!;

    }
}
