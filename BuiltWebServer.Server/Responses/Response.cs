using BuiltWebServer.Server.HTTP;
using System.Text;

namespace BuiltWebServer.Server.Responses
{
    public class Response
    {
        public StatusCode StatusCode { get; }
        public HeaderCollection Headers { get; } = new();
        public StringBuilder Body { get; } = new();

        public Action<Request, Response>? PreRenderAction { get; set; }

        public Response(StatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"HTTP/1.1 {(int)StatusCode}");

            foreach (var header in Headers)
                sb.AppendLine(header.ToString());

            sb.AppendLine();
            sb.Append(Body.ToString());

            return sb.ToString();
        }
    }
}
