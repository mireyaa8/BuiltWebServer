using System.Collections.Generic;

namespace BuiltWebServer.Server.HTTP
{
    public class HeaderCollection
    {
        private readonly Dictionary<string, Header> headers;

        public HeaderCollection()
        {
            headers = new Dictionary<string, Header>();
        }

        public int Count => headers.Count;

        public void Add(Header header)
        {
            headers[header.Name] = header;
        }

        public IEnumerable<Header> All()
        {
            return headers.Values;
        }
    }
}
