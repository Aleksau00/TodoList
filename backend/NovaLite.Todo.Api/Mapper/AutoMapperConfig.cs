using AutoMapper;

namespace NovaLite.Todo.Api.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
