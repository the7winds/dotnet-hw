namespace Roguelike
{
    using System;
    using System.Collections.Generic;

    public partial class Game
    {
        public class Map
        {
            private readonly ISet<(int, int)> walls;

            private readonly Tuple<int, int> spawn;

            public Map(string mapFilename)
            {
                this.walls = new HashSet<(int, int)>();

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
                                    this.walls.Add((x, y));
                                    break;
                                case '@':
                                    this.spawn = new Tuple<int, int>(x, y);
                                    break;
                            }
                        }
                    }
                }
            }

            public ISet<(int, int)> Walls => this.walls;

            public Tuple<int, int> Spawn => this.spawn;

            public bool IsFree((int, int) pos) => !this.Walls.Contains(pos);
        }
    }
}