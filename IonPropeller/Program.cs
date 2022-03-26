using IonPropeller.RemoteServices.Mapbox;
using IonPropeller.Services.Geocoding;
using IonPropeller.Services.Journey;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// configure built-in services

builder.Services.AddMemoryCache();

// configure application services

builder.Services.AddSingleton(sp => new MapboxConfiguration
{
    AccessToken = sp.GetRequiredService<IConfiguration>().GetValue<string>("mapbox_access_token")
});

builder.Services.AddSingleton<MapboxClient>();
builder.Services.AddSingleton<IGeocodingService, MapboxGeocodingService>();

builder.Services.AddSingleton<IGroupService, FakeGroupService>();

// configure web host

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyMethod();
    policyBuilder.AllowAnyOrigin();

    // TODO: configure allowed origins
});

app.Run();