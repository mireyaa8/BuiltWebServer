using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BuiltWebServer.Server.HTTP;

namespace BuiltWebServer.Server
{
    public class HttpServer
    {
        private const int BufferSize = 1024;
        private const int MaxRequestSize = 10 * 1024;
        private readonly TcpListener listener;
        public HttpServer(IPAddress ipAddress, int port)
        {
            listener = new TcpListener(ipAddress, port);
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
                Console.WriteLine(requestText);

                Response response = new Response(StatusCode.Ok, "Hello from BuiltWebServer!");
                byte[] responseBytes = Encoding.UTF8.GetBytes(response.ToString());

                stream.Write(responseBytes, 0, responseBytes.Length);

                stream.Close();
                client.Close();
            }
        }
        private string ReadRequest(NetworkStream stream)
        {
            byte[] buffer = new byte[BufferSize];
            StringBuilder sb = new StringBuilder();
            int totalBytes = 0;

            int bytesRead;
            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                totalBytes += bytesRead;

                if (totalBytes > MaxRequestSize)
                {
                    throw new InvalidOperationException("Request too large.");
                }

                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            } while (stream.DataAvailable);

            return sb.ToString();
        }
    }
}
