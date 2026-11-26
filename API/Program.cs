using Application.UseCases;
using Domain.Interfaces;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IParkingSpaceRepository, InMemoryParkingSpaceRepository>();
builder.Services.AddScoped<CreateParkingSpaceUseCase>();
builder.Services.AddScoped<GetAllSpacesUseCase>();
builder.Services.AddScoped<GetSpaceByIdUseCase>();
builder.Services.AddScoped<UpdateSpaceUseCase>();
builder.Services.AddScoped<OccupySpaceUseCase>();
builder.Services.AddScoped<ReleaseSpaceUseCase>();

var app = builder.Build();


// Configure the HTTP request pipeline.
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

