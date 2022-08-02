using EasyNetQ;
using Fibonacci.Contract;
using Fibonacci.Services;
using Fibonacci.Storage;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IRepo, Repo>();
builder.Services.AddSingleton<ITaskListener, TaskListener>();
builder.Services.AddHttpClient<IHttpClientService, HttpClientService>();// todo верно ли
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddHostedService<FibonacciBackgroundService>();
builder.Services.AddScoped<IValidator<int>, RequestValidator>(); // todo верно ли
builder.Services.Configure<HttpClientSettings>(builder.Configuration.GetSection(nameof(HttpClientSettings)));
builder.Services.AddSingleton(RabbitHutch.CreateBus("host=localhost:5672;username=guest;password=guest"));//todo
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