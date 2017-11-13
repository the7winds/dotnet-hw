namespace Roguelike
{
    using System.Collections.Generic;
    using System.IO;

    public partial class Game
    {
        public class Map
        {
            private readonly ISet<(int, int)> walls;

            private readonly (int, int) spawn;

            public Map(string mapFilename)
            {
                this.walls = new HashSet<(int, int)>();

                var lines = File.ReadAllLines(mapFilename);

                for (int y = 0; y < lines.Length; y++)
                {
                    var line = lines[y];

                    for (int x = 0; x < line.Length; x++)
                    {
                        switch (line[x])
                        {
                            case '#':
                                this.walls.Add((x, y));
                                break;
                            case '@':
                                this.spawn = (x, y);
                                break;
                        }
                    }
                }
            }

            public ISet<(int, int)> Walls => this.walls;

            public (int, int) Spawn => this.spawn;

            public bool IsFree((int, int) pos) => !this.Walls.Contains(pos);
        }
    }
}