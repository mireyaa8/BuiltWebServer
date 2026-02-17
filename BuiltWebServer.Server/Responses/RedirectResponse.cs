using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server.Responses
{
    public class RedirectResponse : Response
    {
        public RedirectResponse(string url)
            : base(StatusCode.Found)
        {
            Headers.Add(Header.Location, url);
        }
    }
}
