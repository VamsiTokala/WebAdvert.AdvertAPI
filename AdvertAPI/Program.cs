using AdvertAPI.HealthChecks;
using AdvertAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IAdvertStorageService,DynamoDBAdvertStorage>();
/*builder.Services.AddHealthChecks(checks =>
{
    checks.AddCheck<StorageHealthCheck>("Storage", new System.TimeSpan(0,1,0)); // healthchecks get cached, giving time
    //checks.AddCheck<StorageHealthCheck>("Storage")
}

);*/

builder.Services.AddHealthChecks()
    .AddCheck<StorageHealthCheck>("Storage");

//builder.Services.AddAutoMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseAuthorization();

app.MapControllers();

app.Run();
