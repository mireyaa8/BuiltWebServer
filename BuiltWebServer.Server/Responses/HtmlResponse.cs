using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html,
            Action<Request, Response>? action = null)
            : base(html, ContentType.Html, action) { }
    }
}