namespace TempApp.Services
{
    public class TempProvider : ITempProvider
    {
        private Random r;

        public TempProvider()
        {
            r = new Random();
        }

        public double GetTemp()
        {
            return (double)r.Next(0, 10000) / 100.0;
        }
    }
}
