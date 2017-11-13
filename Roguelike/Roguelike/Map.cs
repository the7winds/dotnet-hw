namespace Roguelike
{
    using System;
    using System.Collections.Generic;

    public partial class Game
    {
        public class Map
        {
            private readonly ISet<Tuple<int, int>> walls;

            private readonly Tuple<int, int> spawn;

            public Map(string mapFilename)
            {
                this.walls = new HashSet<Tuple<int, int>>();

                using (var mapFile = new System.IO.StreamReader(mapFilename))
                {
                    for (int y = 0; !mapFile.EndOfStream; ++y)
                    {
                        var line = mapFile.ReadLine();
                        for (int x = 0; x < line.Length; ++x)
                        {
                            switch (line[x])
                            {
                                case '#':
                                    this.walls.Add(new Tuple<int, int>(x, y));
                                    break;
                                case '@':
                                    this.spawn = new Tuple<int, int>(x, y);
                                    break;
                            }
                        }
                    }
                }
            }

            public ISet<Tuple<int, int>> Walls => this.walls;

            public Tuple<int, int> Spawn => this.spawn;

            public bool IsFree(Tuple<int, int> newPosition) => !this.Walls.Contains(newPosition);
        }
    }
}