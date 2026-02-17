using System.Text;
using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server.Responses
{
    public class ContentResponse : Response
    {
        public ContentResponse(string content, string type,
            Action<Request, Response>? action = null)
            : base(StatusCode.Ok)
        {
            Headers.Add(Header.ContentTypeHeader, type);
            Body.Append(content);
            PreRenderAction = action;
        }

        public override string ToString()
        {
            if (Body.Length > 0)
            {
                Headers.Add(Header.ContentLength,
                    Encoding.UTF8.GetByteCount(Body.ToString()).ToString());
            }

            return base.ToString();
        }
    }
}
