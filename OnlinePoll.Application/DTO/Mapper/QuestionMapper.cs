using OnlinePoll.Domain.Entities;

namespace OnlinePoll.Application.DTO.Mapper
{
    public static class QuestionMapper
    {
        public static Question ToDomain(QuestionDto dto)
        {
            return new Question
            {
                QuestionText = dto.QuestionText,
                PollId = dto.PollId,
            };
        }
        public static QuestionDto ToDto(Question entity)
        {
            return new QuestionDto
            {
                QuestionText = entity.QuestionText,
                PollId = entity.PollId,
            };
        }
    }
}
