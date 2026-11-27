using API.Middlewares;
using Application.UseCases;
using Domain.Interfaces;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IParkingSpaceRepository, InMemoryParkingSpaceRepository>();
builder.Services.AddScoped<CreateParkingSpaceUseCase>();
builder.Services.AddScoped<GetAllSpacesUseCase>();
builder.Services.AddScoped<GetSpaceByIdUseCase>();
builder.Services.AddScoped<UpdateSpaceUseCase>();
builder.Services.AddScoped<OccupySpaceUseCase>();
builder.Services.AddScoped<ReleaseSpaceUseCase>();
builder.Services.AddScoped<DeleteSpaceUseCase>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Microsserviço gerenciador de vagas para bicicletas e patinetes - API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

