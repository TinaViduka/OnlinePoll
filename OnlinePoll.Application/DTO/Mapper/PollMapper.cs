using OnlinePoll.Domain.Entities;
using System.Reflection;

namespace OnlinePoll.Application.DTO.Mapper
{
    public static class PollMapper
    {
        public static Poll ToDomain(PollDto dto)
        {
            return new Poll
            {
                Title = dto.Title,
                Description = dto.Description,
            };
        }
        public static PollDto ToDto(Poll entity)
        {
            return new PollDto
            {
                Title = entity.Title,
                Description = entity.Description,
            };
        }

    }
}
