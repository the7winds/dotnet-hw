namespace Roguelike
{
    public partial class Game
    {
        public class Hero
        {
            public Hero((int, int) spawn) => this.Position = spawn;

            public (int, int) Position { get; set; }
        }
    }
}