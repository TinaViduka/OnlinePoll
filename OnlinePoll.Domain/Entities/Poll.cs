namespace OnlinePoll.Domain.Entities
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
