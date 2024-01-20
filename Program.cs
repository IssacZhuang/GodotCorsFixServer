using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "content",
});
//builder.Services.AddCors();

var app = builder.Build();
//app.UseHttpsRedirection();

//app.UseCors();
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    OnPrepareResponse = (context) =>
    {
        string fileName = context.File.Name;
        app.Logger.LogInformation(fileName);

        context.Context.Response.Headers.Append("Cross-Origin-Embedder-Policy", "require-corp");
        context.Context.Response.Headers.Append("Cross-Origin-Opener-Policy", "same-origin");

    }
});

app.MapDefaultControllerRoute();
app.MapGet("/", () =>
{

    return "Hello World!";
});


app.Run();
