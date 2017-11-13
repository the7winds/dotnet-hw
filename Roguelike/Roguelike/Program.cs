namespace Roguelike
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mapFilename = "map.txt";

            if (args.Length == 1)
            {
                mapFilename = args[0];
            }

            if (!System.IO.File.Exists(mapFilename))
            {
                System.Console.WriteLine($"can't find {mapFilename}");
                System.Environment.Exit(1);
            }

            var game = new Game(mapFilename)
            {
                Printer = new PrettyPrinter()
            };

            var eventLoop = new EventLoop();

            eventLoop.OnStart += game.OnStart;
            eventLoop.OnMove += game.OnMove;
            eventLoop.OnDefault += game.OnDefault;

            eventLoop.Run();

            eventLoop.OnStart -= game.OnStart;
            eventLoop.OnMove -= game.OnMove;
            eventLoop.OnDefault -= game.OnDefault;
        }
    }
}
