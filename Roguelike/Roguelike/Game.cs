namespace Roguelike
{
    public partial class Game
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
            var newPosition = (x: this.hero.Position.Item1 + dx, y: this.hero.Position.Item2 + dy);

            if (this.map.IsFree(newPosition))
            {
                this.hero.Position = newPosition;
            }

            this.Printer.Draw(this.hero);
        }

        public void OnDefault() => this.Printer.ResetKeypress();
    }
}