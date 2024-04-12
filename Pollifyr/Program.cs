namespace Pollifyr
{
    public abstract class Program
    {
        private static readonly Startup Startup = new();

        public static async Task Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Pollifyr");
            Console.WriteLine($"Copyright © 2023-{DateTime.UtcNow.Year} mxritz.xyz");
            Console.WriteLine();

            await Startup.Init(args);
            await Startup.Start();
        }
    }
}




