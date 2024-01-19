using AutoMapper;
using NovaLite.Todo.Api.DTOs;
using NovaLite.Todo.Api.Model;

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
        }
    }
}
