using EasyNetQ;
using FibonacciSecond.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ICalculateSum, CalculateSum>();
builder.Services.AddScoped<IMessagesBus, MessagesBus>();
builder.Services.AddSingleton(RabbitHutch.CreateBus("host=localhost:15672;virtualHost=/;username=user;password=password"));//todo
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();