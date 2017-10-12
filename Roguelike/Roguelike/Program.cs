using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventLoop = new EventLoop();

            eventLoop.OnUp += delegate { Console.WriteLine("up"); };
            eventLoop.OnDown += delegate { Console.WriteLine("down"); };
            eventLoop.OnLeft += delegate { Console.WriteLine("left"); };
            eventLoop.OnRight += delegate { Console.WriteLine("right"); };

            eventLoop.Run();
        }
    }
}
