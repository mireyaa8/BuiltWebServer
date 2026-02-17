using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server.Responses
{
    public class UnauthorizedResponse : Response
    {
        public UnauthorizedResponse() : base(StatusCode.Unauthorized) { }
    }
}
