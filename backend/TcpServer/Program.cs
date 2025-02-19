using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class TcpServer
{
    static async Task Main()
    {
        int port = 5000;
        TcpListener server = new TcpListener(IPAddress.Any, port);
        server.Start();

        Console.WriteLine($"[SERVER] Listening on port {port}...");

        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            Console.WriteLine("[SERVER] Client connected!");

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
                    Console.WriteLine($"[SERVER] Received: {request}");

                    string response = ProcessCommand(request);

                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseData, 0, responseData.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SERVER] Error: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("[SERVER] Client disconnected.");
            }
        }
    }

    static string ProcessCommand(string command)
    {
        switch (command)
        {
            case "GET_TEMP":
                return "Temperature: 25.3°C";
            case "GET_STATUS":
                return "Status: Active";
            default:
                return "Error: Unknown command";
        }
    }
}
