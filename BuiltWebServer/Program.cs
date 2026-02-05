using BuiltWebServer.Server;
using System.Net;
using System.Net.Sockets;
using System.Net.Sockets;
using System.Text;
namespace BuiltWebServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8080;
            HttpServer server = new HttpServer(ipAddress, port);
            server.Start();
        }
    }
}
