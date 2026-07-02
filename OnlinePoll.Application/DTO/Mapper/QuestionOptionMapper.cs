using OnlinePoll.Domain.Entities;

namespace OnlinePoll.Application.DTO.Mapper
{
    public static class QuestionOptionMapper
    {
        public static QuestionOption ToDomain(QuestionOptionDto dto)
        {
            return new QuestionOption
            {
                OptionText = dto.OptionText
            };
        }
        public static QuestionOptionDto ToDto(QuestionOption entity)
        {
            return new QuestionOptionDto
            {
                OptionText = entity.OptionText
            };
        }
    }
}
