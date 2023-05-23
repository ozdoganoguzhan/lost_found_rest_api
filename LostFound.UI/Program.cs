using Microsoft.EntityFrameworkCore;
using LostFound.Core.Entities;
using LostFound.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<LFDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("LostFoundSQL")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();


var app = builder.Build();

app.UseCors(options =>
options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();