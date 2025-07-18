using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Registrazioni.Business;
using Registrazioni.Business.Abstraction;
using Registrazioni.Business.kafka;
using Registrazioni.Business.kafka.kafkaAbstract;
using Registrazioni.Business.kafka.MessageHandler;
using Registrazioni.Business.Kafka;
using Registrazioni.Business.Profiles;
using Registrazioni.Repository;
using Registrazioni.Repository.Abstraction;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();
builder.Services.AddDbContext<RegistrazioniDbContext>(options =>
    options.UseSqlServer("name=ConnectionStrings:RegistrazioneDbContext"));//,b => b.MigrationsAssembly("Registrazioni.Api")));

builder.Services.AddAutoMapper(typeof(AssemblyMarker));

builder.Services.Configure<KafkaSettings>(
    builder.Configuration.GetSection("Kafka"));
builder.Services.AddSingleton(provider =>
{
    var settings = provider.GetRequiredService<IOptions<KafkaSettings>>().Value;

    return new ConsumerConfig
    {
        BootstrapServers = "localhost:9092",
        GroupId = "votazione",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = true
    };
});


builder.Services.AddScoped<KafkaVotoConsumer>();
builder.Services.AddHostedService<TransactionalOutboxProcessor>();
builder.Services.AddSingleton<IConsumerClient<string,string>, KafkaConsumerClient>();


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
