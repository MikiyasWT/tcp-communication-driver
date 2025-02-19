using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpWebApi.Contracts;

namespace TcpWebApi.Controllers
{
    [Route("api/tcp")]
    [ApiController]
    public class TcpController : ControllerBase
    {
        private readonly ILoggingService _logger;
        private readonly string serverIp = "127.0.0.1";
        private readonly int serverPort = 5000;

        public TcpController(ILoggingService logger)
        {
            _logger = logger;
        }

        private async Task<string> SendTcpCommand(string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(serverIp, serverPort))
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(command);
                    await stream.WriteAsync(data, 0, data.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    _logger.LogInformation("[WEB API] Sent command: {Command}, Received response: {Response}", command, response);
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[WEB API] Error sending TCP command");
                return $"Error: {ex.Message}";
            }
        }

        [HttpGet("get-temp")]
        public async Task<IActionResult> GetTemperature()
        {
            _logger.LogInformation("[WEB API] Received request: GET_TEMP");
            string response = await SendTcpCommand("GET_TEMP");
            return Ok(new { message = response });
        }

        [HttpGet("get-status")]
        public async Task<IActionResult> GetStatus()
        {
            _logger.LogInformation("[WEB API] Received request: GET_STATUS");
            string response = await SendTcpCommand("GET_STATUS");
            return Ok(new { message = response });
        }
    }
}
