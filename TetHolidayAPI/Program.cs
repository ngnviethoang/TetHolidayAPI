using Microsoft.EntityFrameworkCore;
using TetHolidayAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<TetHolidayDbContext>(opt => opt.UseSqlite("Data Source=tetholiday.db"));

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
var db = scope.ServiceProvider.GetRequiredService<TetHolidayDbContext>();
await db.Database.MigrateAsync();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
await app.RunAsync();