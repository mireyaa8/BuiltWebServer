using BuiltWebServer.Server.HTTP;
using BuiltWebServer.Server.Responses;

namespace BuiltWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable MapGet(string url, Response response);
        IRoutingTable MapPost(string url, Response response);
        Response MatchRequest(Request request);
    }
}
