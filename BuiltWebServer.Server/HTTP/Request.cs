using System.Web;

namespace BuiltWebServer.Server.HTTP
{
    public class Request
    {
        public Method Method { get; private set; }
        public string Url { get; private set; }
        public HeaderCollection Headers { get; private set; } = new();
        public string Body { get; private set; }
        public Dictionary<string, string> FormData { get; private set; } = new();

        public static Request Parse(string request)
        {
            string[] lines = request.Split("\r\n");
            string[] start = lines[0].Split(' ');

            Request req = new()
            {
                Method = Enum.Parse<Method>(start[0]),
                Url = start[1]
            };

            int i = 1;
            while (!string.IsNullOrEmpty(lines[i]))
            {
                string[] parts = lines[i].Split(':', 2);
                req.Headers.Add(parts[0], parts[1].Trim());
                i++;
            }

            req.Body = string.Join("\n", lines.Skip(i + 1));
            req.FormData = ParseForm(req.Headers, req.Body);

            return req;
        }

        private static Dictionary<string, string> ParseForm(HeaderCollection headers, string body)
        {
            Dictionary<string, string> form = new();

            if (!headers.Contains(Header.ContentTypeHeader)) return form;
            if (headers[Header.ContentTypeHeader].Value != ContentType.Form) return form;

            foreach (var pair in body.Split('&'))
            {
                var parts = pair.Split('=');
                form[HttpUtility.UrlDecode(parts[0])] =
                    HttpUtility.UrlDecode(parts[1]);
            }

            return form;
        }
    }
}
