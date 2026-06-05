namespace OnlinePoll.Application.DTO
{
    public class PollDto
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime Created { get; set; }
    }
}
