using Microsoft.EntityFrameworkCore;
using TestAndal.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject DbContext
builder.Services.AddDbContext<AndalDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AndalsDbConnectionString")));

builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (option) => {
        option.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
