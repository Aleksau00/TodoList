using AutoMapper;
using Novalite.Todo.Shared.DTOs;
using NovaLite.Todo.Shared.DTOs;
using Novalite.Todo.Shared.Model;
using NovaLite.Todo.Shared.Model;

namespace NovaLite.Todo.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoListDTO, TodoList>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<TodoItemDTO, TodoItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<EditTodoItemDTO, TodoItem>();
            CreateMap<TodoReminderRequest, TodoReminder>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        }
    }
}
