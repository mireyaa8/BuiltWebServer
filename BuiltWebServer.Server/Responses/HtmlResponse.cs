using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html)
            : base(html, ContentType.Html) { }
    }
}
