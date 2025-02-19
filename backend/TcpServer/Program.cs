using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Serilog;

class TcpServer
{
    private static ILogger log;
    private static Random random = new Random();

    static async Task Main()
    {
    log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/tcpserver.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        int port = 5000;
        TcpListener server = new TcpListener(IPAddress.Any, port);
        server.Start();

        log.Information($"[SERVER] Listening on port {port}...");

        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            log.Information($"[SERVER] Client connected from {remoteEndPoint?.Address}:{remoteEndPoint?.Port}");

            // Handle each client in a new task
            Task.Run(() => HandleClient(client));
        }
    }

    static async Task HandleClient(TcpClient client)
    {
        using (NetworkStream stream = client.GetStream())
        {
            try
            {

                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Client disconnected

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    log.Information($"[SERVER] Received: {request}");

                    string response = ProcessCommand(request);

                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseData, 0, responseData.Length);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "[SERVER] An error occurred");
            }
            finally
            {
                client.Close();
                log.Information("[SERVER] Client disconnected.");
            }
        }
    }

    static string ProcessCommand(string command)
    {
        switch (command)
        {
            case "GET_TEMP":
                //temperature will be show randomly from 14 to 47
                int temperature = random.Next(14, 47);
                return $"Temperature: {temperature}°C";
            case "GET_STATUS":
                return "Status: Active";
            default:
                return "Error: Unknown command";
        }
    }
}
