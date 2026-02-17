namespace BuiltWebServer.Server.HTTP
{
    public class Header
    {
        public const string ContentTypeHeader = "Content-Type";
        public const string ContentLength = "Content-Length";
        public const string Location = "Location";

        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; set; }

        public override string ToString() => $"{Name}: {Value}";
    }
}
