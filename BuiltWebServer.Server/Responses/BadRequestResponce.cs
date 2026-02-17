using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server.Responses
{
    public class BadRequestResponse : Response
    {
        public BadRequestResponse() : base(StatusCode.BadRequest) { }
    }
}
