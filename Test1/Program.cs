namespace Test1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddTransient<IShowTime, ShortTime>();
            builder.Services.AddTransient<IShowTime, LongTime>();
            var app = builder.Build();

            // <snippet_UseWebSockets>
            var webSocketOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            };
            app.UseWebSockets(webSocketOptions);
            // </snippet_UseWebSockets>

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapControllers();
            app.UseMiddleware<ShowTimeMiddleware>();
            app.Run();
        }
    }
}