using API.Middlewares;
using MicroMob.Core.Services;
using MicroMob.Core.Interfaces;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IParkingSpaceRepository, InMemoryParkingSpaceRepository>();
builder.Services.AddScoped<CreateSpaceService>();
builder.Services.AddScoped<GetAllSpacesService>();
builder.Services.AddScoped<GetSpaceService>();
builder.Services.AddScoped<UpdateSpaceService>();
builder.Services.AddScoped<OccupySpaceService>();
builder.Services.AddScoped<ReleaseSpaceService>();
builder.Services.AddScoped<DeleteSpaceService>();

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

