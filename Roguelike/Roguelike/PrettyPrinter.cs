namespace Roguelike
{
    using System;

    public class PrettyPrinter
    {
        private readonly int width;
        private readonly int height;
        private Tuple<int, int> heroPosition;

        public PrettyPrinter(int width = 100, int height = 80)
        {
            this.width = width;
            this.height = height;

            Console.CursorVisible = false;
            Console.Clear();
        }

        public void Draw(Game.Hero hero)
        {
            if (this.heroPosition != null)
            {
                Console.SetCursorPosition(this.heroPosition.Item1, this.heroPosition.Item2);
                Console.Write(' ');
            }

            this.heroPosition = this.Transform(hero.Position);
            Console.SetCursorPosition(this.heroPosition.Item1, this.heroPosition.Item2);
            Console.Write('@');
            Console.SetCursorPosition(this.heroPosition.Item1, this.heroPosition.Item2);
        }

        public void Draw(Game.Map map)
        {
            Console.SetCursorPosition(0, 0);
            foreach (Tuple<int, int> block in map.Walls)
            {
                var position = this.Transform(block);
                Console.SetCursorPosition(position.Item1, position.Item2);
                Console.Write('#');
            }
        }

        public void ResetKeypress()
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write("@");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

        private Tuple<int, int> Transform(Tuple<int, int> position)
        {
            var x = (position.Item1 + this.width) % this.width;
            var y = (position.Item2 + this.height) % this.height;
            return new Tuple<int, int>(x, y);
        }
    }
}