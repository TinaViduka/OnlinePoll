using OnlinePoll.Domain.Entities;

namespace OnlinePoll.Application.DTO.Mapper
{
    public static class AnswerMapper
    {
        public static Answer ToDomain(AnswerDto dto)
        {
            return new Answer
            {
                AnswerText = dto.AnswerText,
                QuestionId = dto.QuestionId,
                QuestionOptionId = dto.QuestionOptionId
            };
        }

        public static AnswerDto ToDto(Answer entity)
        {
            return new AnswerDto
            {
                AnswerText = entity.AnswerText,
                QuestionId = entity.QuestionId,
                QuestionOptionId = entity.QuestionOptionId
            };
        }
    }
}
