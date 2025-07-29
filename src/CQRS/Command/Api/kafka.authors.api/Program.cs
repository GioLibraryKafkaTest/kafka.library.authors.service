using kafka.authors.application;
using infraestructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfraestructureService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference(opt =>
  {
    opt.Title = "Microservices command scalar";
    opt.DarkMode = true;
    opt.Theme = ScalarTheme.DeepSpace;
    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
  });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
