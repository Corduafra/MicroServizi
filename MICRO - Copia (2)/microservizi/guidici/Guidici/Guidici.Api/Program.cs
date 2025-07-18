using Guidici.Business.Abstraction;
using Guidici.Business;
using Guidici.Repository.Abstraction;
using Guidici.Repository;
using Microsoft.EntityFrameworkCore;
using Guidici.Business.Profiles;
using Guidici.Business.Kafka;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();


object value = builder.Services.AddAutoMapper(typeof(AssemblyMarker));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<GuidiciDbContext>(options =>
    options.UseSqlServer("name=ConnectionStrings:GuidiciDbContext"));





// implementare un producer Service personale
builder.Services.AddHostedService<OutboxHostedService>();
builder.Services.AddScoped<TransactionalOutboxProcessor>();
builder.Services.AddSingleton<IProducerClient<string, string>, ProducerClient>(); 


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
