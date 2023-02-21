using Microsoft.AspNetCore.Mvc;
using MW_and_DI_Training;
using System.Net.WebSockets;
using System.Text;

namespace Test1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSocketsController : ControllerBase
    {
        private readonly ILogger<WebSocketsController> _logger;

        public WebSocketsController(ILogger<WebSocketsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _logger.Log(LogLevel.Information, "WebSocket connection established");
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            int i = 0;
            while (i<10)
            {
                var time = new ShortTime().GetTime();
                var serverMsg = Encoding.UTF8.GetBytes($"Time is {time}. I = {i}");
                await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length),
                                          WebSocketMessageType.Text,
                                          true,
                                          CancellationToken.None);
                await Task.Delay(2000);
                i++;
            } 
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Just", CancellationToken.None);

        }
    }
}