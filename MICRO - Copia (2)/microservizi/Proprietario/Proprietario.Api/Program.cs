using Microsoft.EntityFrameworkCore;
using Proprietario.Business;
using Proprietario.Business.Abstraction;
using Proprietario.Repository;
using Proprietario.Repository.Abstraction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient<Registrazioni.ClientHttp.Abstraction.IClientHttp, 
 Registrazioni.ClientHttp.ClientHttp>("RegistrazioniClientHttp", httpClient => {
     httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("RegistrazioniClientHttp:BaseAddress").Value);
 }) ;

//builder.Services.AddHttpClient<ClientHttp>("RegistrazioneClientHttp",
//httpClient =>{ httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("RegistrazioniClientHttp:BaseAddress").Value); }) ;


builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();

builder.Services.AddDbContext<ProprietarioDbContext>(options =>
    options.UseSqlServer("name=ConnectionStrings:ProprietarioDbContext"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
