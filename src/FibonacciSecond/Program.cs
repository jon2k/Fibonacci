using System.Reflection;
using Common.Contract;
using EasyNetQ;
using FibonacciSecond.Application.Command;
using FibonacciSecond.Domain;
using FibonacciSecond.Infrastructure;
using FibonacciSecond.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IRequestHandler<CalculateCommand, MessageResponseFib>, CalculateCommandHandler>();
builder.Services.AddScoped<ICalculateSum, CalculateSum>();
builder.Services.AddScoped<IMessagesBus, MessagesBus>();
builder.Services.AddSingleton(RabbitHutch.CreateBus(builder.Configuration.GetConnectionString("RabbitMq")));
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