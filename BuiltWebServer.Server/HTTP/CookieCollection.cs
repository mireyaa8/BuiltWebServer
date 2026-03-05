using System.Collections;
using System.Collections.Generic;

namespace BuiltWebServer.Server.HTTP
{
    public class CookieCollection : IEnumerable<Cookie>
    {
        private readonly Dictionary<string, Cookie> cookies = new();

        public void Add(Cookie cookie)
        {
            cookies[cookie.Name] = cookie;
        }

        public bool Contains(string name)
        {
            return cookies.ContainsKey(name);
        }

        public Cookie this[string name]
        {
            get => cookies[name];
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}