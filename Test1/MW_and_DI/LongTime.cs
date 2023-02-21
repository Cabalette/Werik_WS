namespace Test1
{
    public class LongTime : IShowTime
    {
        public string GetTime()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}
