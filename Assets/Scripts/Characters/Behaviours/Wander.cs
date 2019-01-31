using RogueSharp;
using BannersOfRogues.Interfaces;
using BannersOfRogues.Characters;
using BannersOfRogues.Systems;
using BannersOfRogues.World;
using System.Collections.Generic;
using RogueSharp.Algorithms;

namespace BannersOfRogues.Characters.Behaviours {
    public class Wander : IBehaviour {
        public bool Act(Enemy enemy, CommandSystem commandSystem, Game game) {
            // Dungeon world = game.World;
            
            // if (enemy.WanderPath == null || enemy.WanderPath.Length == 0) {
            //     enemy.WanderPath = new Path(world.Dijkstra.findPath(world.GetCell(enemy.X, enemy.Y)));
            // }

            // if (enemy.WanderPath != null) {
            //     try
            //     {
            //         commandSystem.MoveEnemy(enemy, enemy.WanderPath.StepForward() as Cell);
            //     }
            //     catch (NoMoreStepsException)
            //     {
            //         enemy.WanderPath = null;
            //         game.Logger.Add(enemy.Name + " не может пройти.");
            //     }
            // }
            
            return true;
        }
    }
}