using BannersOfRogues.Systems;
using BannersOfRogues.Characters.Behaviours;
using BannersOfRogues.Core;
using BannersOfRogues.World;

using RogueSharp;

namespace BannersOfRogues.Characters {
    public class Enemy : Actor {
        /// <summary>
        ///кол-во ходов, когда враг насторожен
        /// </summary>
        public int? TurnsAlerted { get; set; } 
        public Path WanderPath { get; set; }

        public Enemy(Game game) : base(game) {}

        public void DrawStats(int pos) {
            game.DrawEnemyStats(this, pos);
        }

        public virtual void PerformAction(CommandSystem commandSystem) {
            var wander = new Wander();
            var moveAndAttack = new MoveAndAttack();

            Map world = game.MapManager.Map;
            Player player = game.Player;
            FieldOfView fov = new FieldOfView(world);

            wander.Act(this, commandSystem, game);
            // fov.ComputeFov(this.X, this.Y, this.Awareness, true);
            // if (fov.IsInFov(player.X, player.Y)) {
            //     moveAndAttack.Act(this, commandSystem, game);
            // } else {
            //     wander.Act(this, commandSystem, game);
            // }
        }
    }
}