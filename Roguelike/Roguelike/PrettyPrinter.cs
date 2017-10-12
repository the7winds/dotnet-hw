namespace Roguelike
{
    using System;

    public class PrettyPrinter
    {
        private readonly int _width;
        private readonly int _height;
        private Tuple<int, int> heroPosition;

        public PrettyPrinter(int width = 100, int height = 80)
        {
            this._width = width;
            this._height = height;

            Console.CursorVisible = false;
            Console.Clear();
        }

        private Tuple<int, int> Transform(Tuple<int, int> position)
        {        
            var x = (position.Item1 + _width) % _width;
            var y = (position.Item2 + _height) % _height;
            return new Tuple<int, int>(x, y);
        }

        public void Draw(Game.Hero hero)
        {
            if (heroPosition != null)
            {
                Console.SetCursorPosition(heroPosition.Item1, heroPosition.Item2);
                Console.Write(' ');
            }

            heroPosition = Transform(hero.Position);
            Console.SetCursorPosition(heroPosition.Item1, heroPosition.Item2);
            Console.Write('@');
            Console.SetCursorPosition(heroPosition.Item1, heroPosition.Item2);
        }

        public void Draw(Game.Map map)
        {
            Console.SetCursorPosition(0, 0);
            foreach (Tuple<int, int> block in map.Walls) {
                var position = Transform(block);
                Console.SetCursorPosition(position.Item1, position.Item2);
                Console.Write('#');
            }
        }
    }
}