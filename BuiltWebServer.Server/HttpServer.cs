using System.Net;
using System.Net.Sockets;
using System.Text;
using BuiltWebServer.Server.HTTP;
using BuiltWebServer.Server.Responses;
using BuiltWebServer.Server.Routing;

namespace BuiltWebServer.Server
{
    public class HttpServer
    {
        private readonly TcpListener listener;
        private readonly IRoutingTable routingTable;

        public HttpServer(Action<IRoutingTable> routes)
        {
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            routingTable = new RoutingTable();
            routes(routingTable);
        }

        public void Start()
        {
            listener.Start();
            Console.WriteLine("Server started on http://localhost:8080");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                string requestText = ReadRequest(stream);
                Request request = Request.Parse(requestText);

                Response response = routingTable.MatchRequest(request);

                response.PreRenderAction?.Invoke(request, response);

                WriteResponse(stream, response);

                stream.Close();
                client.Close();
            }
        }

        private string ReadRequest(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            StringBuilder sb = new StringBuilder();
            int bytes;

            do
            {
                bytes = stream.Read(buffer);
                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            }
            while (stream.DataAvailable);

            return sb.ToString();
        }

        private void WriteResponse(NetworkStream stream, Response response)
        {
            byte[] data = Encoding.UTF8.GetBytes(response.ToString());
            stream.Write(data);
        }
    }
}
