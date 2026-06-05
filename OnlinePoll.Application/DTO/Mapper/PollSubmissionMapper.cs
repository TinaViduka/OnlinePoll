using OnlinePoll.Domain.Entities;

namespace OnlinePoll.Application.DTO.Mapper
{
    public static class PollSubmissionMapper
    {
        public static PollSubmission ToDomain(PollSubmissionDto dto)
        {
            return new PollSubmission
            {
                PollId = dto.PollId,
            };
        }

        public static PollSubmissionDto ToDto(PollSubmission entity)
        {
            return new PollSubmissionDto
            {
                PollId = entity.PollId,
            };
        }
    }
}
