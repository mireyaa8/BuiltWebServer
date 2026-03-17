using BuiltWebServer.Server;
using BuiltWebServer.Server.HTTP;
using BuiltWebServer.Server.Responses;
using BuiltWebServer.Server.Routing;

namespace BuiltWebServer
{
    internal class Program
    {
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
                    .MapGet("/", new TextResponse("Server working"))
                    .MapGet("/Session", new TextResponse("", AsyncSession))
                    .MapGet("/Login", new HtmlResponse(LoginForm))
                    .MapPost("/Login", new HtmlResponse("", AsyncLogin))
                    .MapGet("/Logout", new TextResponse("", AsyncLogout))
                    .MapGet("/UserProfile", new HtmlResponse("", AsyncProfile));
            });

            server.Start();
        }

        private static async void AsyncSession(Request request, Response response)
        {
            await Task.Run(() =>
            {
                if (!request.Cookies.Contains(Session.SessionCookieName))
                {
                    response.Cookies.Add(
                        new Cookie(Session.SessionCookieName, request.Session.Id));
                }

                if (!request.Session.ContainsKey(Session.CurrentDateKey))
                {
                    request.Session[Session.CurrentDateKey] =
                        DateTime.UtcNow.ToString();

                    response.Body.Append("Session created!");
                }
                else
                {
                    response.Body.Append(
                        request.Session[Session.CurrentDateKey]);
                }
            });
        }

        private static async void AsyncLogin(Request request, Response response)
        {
            await Task.Run(() =>
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
            });
        }

        private static async void AsyncLogout(Request request, Response response)
        {
            await Task.Run(() =>
            {
                request.Session.Clear();
                response.Body.Append("Logged out!");
            });
        }

        private static async void AsyncProfile(Request request, Response response)
        {
            await Task.Run(() =>
            {
                if (request.Session.ContainsKey(Session.UserKey))
                {
                    response.Body.Append(
                        $"Hello {request.Session[Session.UserKey]}");
                }
                else
                {
                    response.Body.Append(
                        "Not logged in <a href='/Login'>Login</a>");
                }
            });
        }
    }
}