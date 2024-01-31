using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Api.Controller;
using NovaLite.Todo.Shared.Data;
using Novalite.Todo.Shared.Repos.TodoListRepo;
using NovaLite.Todo.Shared.Repos.TodoListRepo;
using NovaLite.Todo.Shared.Services;
using Azure.Storage.Blobs;
using Novalite.Todo.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
builder.Services.AddScoped<ITodoListService, TodoListService>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ITodoAttachmentRepository, TodoAttachmentRepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<ITodoReminderRepository, TodoReminderRepository>();
builder.Services.AddScoped<BlobStorageService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();



app.UseCors();







// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


