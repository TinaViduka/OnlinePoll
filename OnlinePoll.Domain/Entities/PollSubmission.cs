namespace OnlinePoll.Domain.Entities
{
    public class PollSubmission
    {
        public int Id { get; set; }

        public int PollId { get; set; }
        public Poll Poll { get; set; } = null!;

        public DateTime SubmittedAt { get; set; }

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
