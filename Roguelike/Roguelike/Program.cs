namespace Roguelike
{
    class Program
    {
        static void Main(string[] args)
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

            eventLoop.OnUp += delegate { game.OnMove(0, -1); };
            eventLoop.OnDown += delegate { game.OnMove(0, 1); };
            eventLoop.OnLeft += delegate { game.OnMove(-1, 0); };
            eventLoop.OnRight += delegate { game.OnMove(1, 0); };
            eventLoop.OnStart += game.OnStart;

            eventLoop.Run();
        }
    }
}
