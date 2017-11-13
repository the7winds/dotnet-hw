namespace Roguelike
{
    using System;

    public partial class Game
    {
        public class Hero
        {
            public Hero(Tuple<int, int> spawn) => this.Position = spawn;

            public Tuple<int, int> Position { get; set; }
        }
    }
}