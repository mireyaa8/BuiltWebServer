using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server.Responses
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text,
            Action<Request, Response>? action = null)
            : base(text, ContentType.PlainText, action) { }
    }
}
