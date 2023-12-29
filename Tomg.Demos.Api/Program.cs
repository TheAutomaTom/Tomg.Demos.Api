using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tomg.Demos.Api.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(o=> o.OperationFilter<SwaggerDefaultValues>());
builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(2.0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true; // Provide clients api versions in response headers

    /* These enable different ways for clients to spec which api they want to hit.
        This demo is using an alternative method: url routing, spec'd in the controller. */
    //o.ApiVersionReader = ApiVersionReader.Combine(
    //new QueryStringApiVersionReader()
    //new HeaderApiVersionReader("x-version"),
    //new MediaTypeApiVersionReader("ver"),
    //new UrlSegmentApiVersionReader()
    //);
})
    .AddApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    //o.SubstituteApiVersionInUrl = true;
});
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//var versionSet = app.NewApiVersionSet()
//    .HasApiVersion(new ApiVersion(1))
//    .HasApiVersion(new ApiVersion(2))
//    .Build();

//app.MapGet("api/v{version:apiVersion}/WeatherForecasts", (HttpContext context) =>
//{
//    var apiVersion = context.GetRequestedApiVersion();
//    return WeatherForecastController.Get

//}).WithApiVersionSet(versionSet)
//  .MapToApiVersion(2);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
       var descriptions = app.DescribeApiVersions();
        foreach (var desc in descriptions.Reverse())
        {
            var url = $"/swagger/{desc.GroupName}/swagger.json;";
            var name = desc.GroupName.ToUpperInvariant();
            o.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
