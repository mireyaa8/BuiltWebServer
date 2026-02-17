using BuiltWebServer.Server;
using BuiltWebServer.Server.HTTP;
using BuiltWebServer.Server.Responses;
using BuiltWebServer.Server.Routing;

namespace BuiltWebServer
{
    internal class Program
    {
        private const string HtmlForm = @"<form action='/HTML' method='POST'>
Name: <input type='text' name='Name'/>
Age: <input type='number' name='Age'/>
<input type='submit' value='Save'/>
</form>";



        static void Main()
        {
            HttpServer server = new HttpServer(routes =>
            {
                routes
                    .MapGet("/", new TextResponse("Hello from my server, now with routing table!!!"))
                    .MapGet("/HTML", new HtmlResponse(HtmlForm))
                    .MapPost("/HTML", new TextResponse("", AddFormData))
                    .MapGet("/Redirect", new RedirectResponse("https://softuni.org"));
            });

            server.Start(); 
        }

        private static void AddFormData(Request request, Response response)
        {
            response.Body.Clear();

            foreach (var pair in request.FormData)
            {
                response.Body.AppendLine($"{pair.Key}: {pair.Value}");
            }
        }
    }
}
