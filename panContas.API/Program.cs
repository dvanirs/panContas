using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using panContas.Application.Interfaces;
using panContas.Application.Services;
using panContas.Domain.Interfaces;
using panContas.Infrastructure.Adapters;
using panContas.Infrastructure.Data;
using panContas.Infrastructure.Repositories;

// Permite que o Npgsql aceite DateTime com Kind=Local/Unspecified (comportamento legado)
// Necessário pois o Npgsql 6+ exige Kind=Utc para colunas 'timestamp with time zone'
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IPessoaFisicaRepository, PessoaFisicaRepository>();
builder.Services.AddScoped<IPessoaJuridicaRepository, PessoaJuridicaRepository>();

builder.Services.AddHttpClient<IEnderecoExternalPort, ViaCepAdapter>();

builder.Services.AddScoped<IPessoaFisicaService, PessoaFisicaService>();
builder.Services.AddScoped<IPessoaJuridicaService, PessoaJuridicaService>();

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
     db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "panContas.API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
