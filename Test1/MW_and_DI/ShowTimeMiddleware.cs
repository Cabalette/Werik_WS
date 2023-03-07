namespace Test1
{
    public class ShowTimeMiddleware
    {
        private readonly RequestDelegate request;

        public ShowTimeMiddleware(RequestDelegate request)
        {
            this.request = request;
        }
        public async Task InvokeAsync(HttpContext context, IEnumerable<IShowTime> showTime)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            if (context.Request.Path == "/ws")
            {
                await request.Invoke(context);
            }
            else if (context.Request.Path == "/long") await context.Response.WriteAsync($"<h1>Time: {showTime.First(q => q.GetType() == typeof(LongTime)).GetTime()}</h1>");
            else await context.Response.WriteAsync($"<h1>Time: {showTime.First(q => q.GetType() == typeof(ShortTime)).GetTime()}</h1>");
        }
    }
}
