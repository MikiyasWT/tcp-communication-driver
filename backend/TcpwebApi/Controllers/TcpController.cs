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
        private readonly string _serverIp = "127.0.0.1";
        private readonly int _serverPort = 5000;

        public TcpController(ILoggingService logger)
        {
            _logger = logger;
        }

        private async Task<string> SendTcpCommand(string command)
        {
            using var client = new TcpClient();
            await client.ConnectAsync(_serverIp, _serverPort);

            using NetworkStream stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(command);
            await stream.WriteAsync(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            _logger.LogInfo("[WEB API] Sent command: {Command}, Received response: {Response}", command, response);
            return response;
        }

        [HttpGet("get-temp")]
        public async Task<IActionResult> GetTemperature()
        {
            string response = await SendTcpCommand("GET_TEMP");
            return Ok(new { message = response });
        }

        [HttpGet("get-status")]
        public async Task<IActionResult> GetStatus()
        {
            string response = await SendTcpCommand("GET_STATUS");
            return Ok(new { message = response });
        }
    }
}
