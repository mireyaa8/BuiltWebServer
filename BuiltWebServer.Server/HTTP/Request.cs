using System;
using System.Linq;

namespace BuiltWebServer.Server.HTTP
{
    public class Request
    {
        public Method Method { get; private set; }

        public string Url { get; private set; }

        public HeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        public static Request Parse(string requestString)
        {
            string[] lines = requestString
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            string[] startLine = lines[0].Split(' ');
            Method method = ParseMethod(startLine[0]);
            string url = startLine[1];

            var headerLines = lines
                .Skip(1)
                .TakeWhile(l => l.Contains(":"))
                .ToArray();

            HeaderCollection headers = ParseHeaders(headerLines);

            string body = string.Join(
                Environment.NewLine,
                lines.Skip(1 + headerLines.Length));

            return new Request
            {
                Method = method,
                Url = url,
                Headers = headers,
                Body = body
            };
        }

        private static Method ParseMethod(string method)
        {
            if (!Enum.TryParse(method, out Method parsed))
            {
                throw new InvalidOperationException("Invalid HTTP method.");
            }

            return parsed;
        }

        private static HeaderCollection ParseHeaders(string[] lines)
        {
            HeaderCollection headers = new HeaderCollection();

            foreach (var line in lines)
            {
                string[] parts = line.Split(':', 2);
                if (parts.Length != 2) continue;

                headers.Add(new Header(parts[0], parts[1].Trim()));
            }

            return headers;
        }
    }
}
