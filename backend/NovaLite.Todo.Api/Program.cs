using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Api.Controller;
using NovaLite.Todo.Shared.Data;
using Novalite.Todo.Shared.Repos.TodoListRepo;
using NovaLite.Todo.Shared.Repos.TodoListRepo;
using NovaLite.Todo.Shared.Services;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Novalite.Todo.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using NovaLite.Todo.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Adds Microsoft Identity platform (Azure AD B2C) support to protect this Api
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
        {
            builder.Configuration.Bind("AzureAdB2C", options);

            options.TokenValidationParameters.NameClaimType = "name";
        },
        options => { builder.Configuration.Bind("AzureAdB2C", options); });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.Requirements.Add(new UserRoleRequirement("Admin")));
    options.AddPolicy("Manager", policy => policy.Requirements.Add(new UserRoleRequirement("Manager")));
    options.AddPolicy("User", policy => policy.Requirements.Add(new UserRoleRequirement("User")));
});

builder.Services.AddSingleton<IAuthorizationHandler, UserRoleAuthorizationHandler>();
// End of the Microsoft Identity platform block    

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
builder.Services.AddScoped<ITodoUserRepository, TodoUserRepository>();
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
    IdentityModelEventSource.ShowPII = true;
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


