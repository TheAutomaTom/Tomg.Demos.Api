using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tomg.Demos.Api.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(o=> o.OperationFilter<SwaggerDefaultValues>());
builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(2.0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true; // Provide clients api versions in response headers
})
    .AddApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
});

var app = builder.Build();

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
