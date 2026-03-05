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

        private const string LoginForm = @"<form action='/Login' method='POST'>
Username: <input name='Username'/>
Password: <input name='Password'/>
<input type='submit' value='Login'/>
</form>";

        static void Main()
        {
            HttpServer server = new HttpServer(routes =>
            {
                routes
                    .MapGet("/", new TextResponse("Hello from my server, now with routing table!!!"))
                    .MapGet("/HTML", new HtmlResponse(HtmlForm))
                    .MapPost("/HTML", new TextResponse("", AddFormData))
                    .MapGet("/Redirect", new RedirectResponse("https://softuni.org"))
                    .MapGet("/Session", new TextResponse("", ShowSession))
                    .MapGet("/Login", new HtmlResponse(LoginForm))
                    .MapPost("/Login", new HtmlResponse("", LoginAction))
                    .MapGet("/Logout", new TextResponse("", LogoutAction))
                    .MapGet("/UserProfile", new HtmlResponse("", UserProfile));
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

        private static void ShowSession(Request request, Response response)
        {
            if (request.Session.ContainsKey(Session.CurrentDateKey))
            {
                response.Body.Append(request.Session[Session.CurrentDateKey]);
            }
            else
            {
                response.Body.Append("Current date stored!");
            }
        }

        private static void LoginAction(Request request, Response response)
        {
            request.Session.Clear();

            string username = request.FormData["Username"];
            string password = request.FormData["Password"];

            if (username == "user" && password == "user123")
            {
                request.Session[Session.UserKey] = username;

                response.Cookies.Add(
                    new Cookie(Session.SessionCookieName, request.Session.Id));

                response.Body.Append("Login successful!");
            }
            else
            {
                response.Body.Append(LoginForm);
            }
        }

        private static void LogoutAction(Request request, Response response)
        {
            request.Session.Clear();
            response.Body.Append("Logged out!");
        }

        private static void UserProfile(Request request, Response response)
        {
            if (request.Session.ContainsKey(Session.UserKey))
            {
                response.Body.Append($"Hello {request.Session[Session.UserKey]}");
            }
            else
            {
                response.Body.Append("Not logged in. <a href='/Login'>Login</a>");
            }
        }
    }
}