using Application;
using Infrastructure;
using WebApi;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPresentation()
                .AddInfrastructure(builder.Configuration)
                .AddApplication();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
