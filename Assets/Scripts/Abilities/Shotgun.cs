using BannersOfRogues.Core;
using BannersOfRogues.Interfaces;
using BannersOfRogues.World;
using BannersOfRogues.Characters;

using RogueSharp;

namespace BannersOfRogues.Abilities {
    public class Shotgun : Ability, ITargetable
    {
        private readonly int attack;
        private readonly int attackChance;
        private readonly int area;

        public Shotgun(Game game, int attack, int attackChance, int area) : base(game) {
            Name = "Дробовик";
            Cooldown = 2;
            CurrCD = 0;
            this.attack = attack;
            this.attackChance = attackChance;
            this.area = area;
        }

        protected override bool PerformAbility() {
            return game.TargetingSystem.SelectArea(this, area);
        }

        public void SelectTarget(Point target) {
            MapManager mapManager = game.MapManager;
            Player player = game.Player;
            
            game.Logger.Add(string.Format("{0} стреляет из {1}", player.Name, Name));
            Actor dummy = new Actor(game)
            {   
                Attack = attack,
                AttackChance = attackChance,
                Name = Name
            };

            foreach (RogueSharp.Cell cell in mapManager.Map.GetCellsInSquare(target.X, target.Y, area)) {
                Enemy enemy = mapManager.GetEnemyAt(cell.X, cell.Y);
                if (enemy != null) {
                    game.CommandSystem.Attack(dummy, enemy);
                }
            }
        }   
    }
}