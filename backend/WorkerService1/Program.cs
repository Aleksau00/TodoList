using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Reminder;
using NovaLite.Todo.Shared.Data;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<ReminderWorker>();


var host = builder.Build();
host.Run();
