using System.Collections;

namespace BuiltWebServer.Server.HTTP
{
    public class HeaderCollection : IEnumerable<Header>
    {
        private readonly Dictionary<string, Header> headers = new();

        public Header this[string name]
        {
            get => headers[name];
            set => headers[name] = value;
        }

        public bool Contains(string name) => headers.ContainsKey(name);

        public void Add(string name, string value)
        {
            headers[name] = new Header(name, value);
        }

        public IEnumerator<Header> GetEnumerator() => headers.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
