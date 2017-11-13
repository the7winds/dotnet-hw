namespace Roguelike
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private Map map;
        private Hero hero;

        public Game(string mapFilename)
        {
            this.map = new Map(mapFilename);
            this.hero = new Hero(this.map.Spawn);
        }

        public PrettyPrinter Printer { get; set; }

        public void OnStart()
        {
            this.Printer.Draw(this.map);
            this.Printer.Draw(this.hero);
        }

        public void OnMove(int dx, int dy)
        {
            var newPosition = new Tuple<int, int>(this.hero.Position.Item1 + dx, this.hero.Position.Item2 + dy);

            if (this.map.IsFree(newPosition))
            {
                this.hero.Position = newPosition;
            }

            this.Printer.Draw(this.hero);
        }

        public void OnDefault() => this.Printer.ResetKeypress();

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

        public class Hero
        {
            public Hero(Tuple<int, int> spawn) => this.Position = spawn;

            public Tuple<int, int> Position { get; set; }
        }
    }
}