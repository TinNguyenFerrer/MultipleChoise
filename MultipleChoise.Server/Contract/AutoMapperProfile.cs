using AutoMapper;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Contract.Dto.UserDto;
using MultipleChoise.Server.Data.Models.Entity;
using System.Runtime.CompilerServices;

namespace MultipleChoise.Server.Contract
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<Result, ResultDto>().ReverseMap();

            // Entyty -> DTO
            CreateMap<Quiz, QuizDto>()
            .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => (int)src.Duration.TotalMinutes));

            // DTO -> Entity
            CreateMap<QuizDto, Quiz>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.DurationInMinutes)));


            // ===================  User Permissions  ===================

            CreateMap<Question, QuestionUserDto>().ReverseMap();
            CreateMap<Answer, AnswerUserDto>().ReverseMap();

            // Entyty -> DTO
            CreateMap<Quiz, QuizUserDto>()
            .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => (int)src.Duration.TotalMinutes));

            // DTO -> Entity
            CreateMap<QuizUserDto, Quiz>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.DurationInMinutes)));
            
            // ==================== End User Permissons ===================
        }
        
    }
}
