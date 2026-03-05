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

        public CookieCollection Cookies { get; private set; } = new();
        public Session Session { get; private set; }

        private static readonly Dictionary<string, Session> Sessions = new();

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

            req.Cookies = ParseCookies(req.Headers);
            req.Session = GetSession(req.Cookies);

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

        private static CookieCollection ParseCookies(HeaderCollection headers)
        {
            CookieCollection cookies = new();

            if (!headers.Contains(Header.CookieHeaderName))
                return cookies;

            var pairs = headers[Header.CookieHeaderName].Value.Split("; ");

            foreach (var pair in pairs)
            {
                var parts = pair.Split('=');
                cookies.Add(new Cookie(parts[0], parts[1]));
            }

            return cookies;
        }

        private static Session GetSession(CookieCollection cookies)
        {
            string sessionId = cookies.Contains(Session.SessionCookieName)
                ? cookies[Session.SessionCookieName].Value
                : Guid.NewGuid().ToString();

            if (!Sessions.ContainsKey(sessionId))
                Sessions[sessionId] = new Session(sessionId);

            return Sessions[sessionId];
        }
    }
}