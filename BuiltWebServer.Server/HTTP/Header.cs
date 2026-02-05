using BuiltWebServer.Server.Common;

namespace BuiltWebServer.Server.HTTP
{
    public class Header
    {
        public Header(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; set; }
    }
}
