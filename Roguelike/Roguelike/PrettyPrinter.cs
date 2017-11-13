namespace Roguelike
{
    using System;

    public class PrettyPrinter
    {
        private readonly int width;
        private readonly int height;

        public PrettyPrinter(int width = 100, int height = 80)
        {
            this.width = width;
            this.height = height;

            Console.CursorVisible = false;
            Console.Clear();
        }

        public void Draw(Game.Hero hero)
        {
            var (heroX, heroY) = this.Transform(hero.Position);

            Console.SetCursorPosition(heroX, heroY);
            Console.Write('@');
            Console.SetCursorPosition(heroX, heroY);
        }

        public void Draw(Game.Map map)
        {
            Console.SetCursorPosition(0, 0);
            foreach (var block in map.Walls)
            {
                var (posX, posY) = block;
                var position = this.Transform(block);
                Console.SetCursorPosition(posX, posY);
                Console.Write('#');
            }
        }

        public void ResetKeypress()
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write("@");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

        private (int, int) Transform((int, int) pos)
        {
            var (posX, posY) = pos;
            var x = (posX + this.width) % this.width;
            var y = (posY + this.height) % this.height;
            return (x, y);
        }
    }
}