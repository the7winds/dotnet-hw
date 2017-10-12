namespace Roguelike
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private Map _map;
        private Hero _hero;

        public PrettyPrinter Printer;

        public class Map
        {
            public readonly ISet<Tuple<int, int>> Walls = new HashSet<Tuple<int, int>>();

            public readonly Tuple<int, int> Spawn;

            public Map(string mapFilename)
            {
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
                                    Walls.Add(new Tuple<int, int>(x, y));
                                    break;
                                case '@':
                                    Spawn = new Tuple<int, int>(x, y);
                                    break;
                            }
                        }
                    }
                }
            }

            public bool IsFree(Tuple<int, int> newPosition) => !Walls.Contains(newPosition);
        }

        public void OnStart()
        {
            Printer.Draw(_map);
            Printer.Draw(_hero);
        }

        public class Hero
        {
            public Tuple<int, int> Position;

            public Hero(Tuple<int, int> spawn) => Position = spawn;
        }

        public Game(string mapFilename)
        {
            this._map = new Map(mapFilename);
            this._hero = new Hero(this._map.Spawn);
        } 

        internal void OnMove(int dx, int dy)
        {
            var newPosition = new Tuple<int, int>(this._hero.Position.Item1 + dx, this._hero.Position.Item2 + dy);

            if (this._map.IsFree(newPosition))
            {
                this._hero.Position = newPosition;
            }

            Printer.Draw(_hero);
        }
    }
}