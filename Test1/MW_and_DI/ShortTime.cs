namespace Test1
{
    public class ShortTime : IShowTime
    {
        public string GetTime()
        {
            return DateTime.Now.ToShortTimeString();
        }
    }
}
