using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Sudoku.WebApi.Infrastructure.Extensions;

public static class UiExtensions
{
    public static IApplicationBuilder UseStaticUi(this WebApplication app)
    {
        var angularRoot = Path.Combine(app.Environment.WebRootPath, "browser");

        app.UseDefaultFiles(new DefaultFilesOptions
        {
            FileProvider = new PhysicalFileProvider(angularRoot),
            DefaultFileNames = ["index.html"]
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(angularRoot),
            RequestPath = ""
        });

        app.MapFallback(async context =>
        {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync(Path.Combine(angularRoot, "index.html"));
        });

        return app;
    }
}