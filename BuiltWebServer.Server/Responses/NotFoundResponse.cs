using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server.Responses
{
    public class NotFoundResponse : Response
    {
        public NotFoundResponse() : base(StatusCode.NotFound) { }
    }
}
