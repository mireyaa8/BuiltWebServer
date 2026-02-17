using BuiltWebServer.Server.HTTP;
using BuiltWebServer.Server.Responses;

namespace BuiltWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<Method, Dictionary<string, Response>> routes =
            new()
            {
                { Method.GET, new() },
                { Method.POST, new() }
            };

        public IRoutingTable MapGet(string url, Response response)
        {
            routes[Method.GET][url] = response;
            return this;
        }

        public IRoutingTable MapPost(string url, Response response)
        {
            routes[Method.POST][url] = response;
            return this;
        }

        public Response MatchRequest(Request request)
        {
            if (routes.ContainsKey(request.Method) &&
                routes[request.Method].ContainsKey(request.Url))
            {
                return routes[request.Method][request.Url];
            }

            return new NotFoundResponse();
        }
    }
}
