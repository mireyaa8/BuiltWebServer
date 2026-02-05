using System.Text;

namespace BuiltWebServer.Server.HTTP
{
    public class Response
    {
        public Response(StatusCode statusCode, string content)
        {
            StatusCode = statusCode;
            Content = content;

            Headers = new HeaderCollection();
            Headers.Add(new Header("Content-Type", "text/plain; charset=UTF-8"));
            Headers.Add(new Header("Content-Length", Encoding.UTF8.GetByteCount(content).ToString()));
        }

        public StatusCode StatusCode { get; }

        public string Content { get; }

        public HeaderCollection Headers { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"HTTP/1.1 {(int)StatusCode} OK");

            foreach (var header in Headers.All())
            {
                sb.AppendLine($"{header.Name}: {header.Value}");
            }

            sb.AppendLine();
            sb.Append(Content);

            return sb.ToString();
        }
    }
}
