using RogueSharp;
using BannersOfRogues.Interfaces;
using BannersOfRogues.Characters;
using BannersOfRogues.Systems;
using BannersOfRogues.World;

namespace BannersOfRogues.Characters.Behaviours {
    public class MoveAndAttack : IBehaviour {
        public bool Act(Enemy enemy, CommandSystem commandSystem, Game game) {
            MapManager mapManager = game.MapManager;
            Player player = game.Player;
            FieldOfView enemyFov = new FieldOfView(mapManager.Map);

            if (!enemy.TurnsAlerted.HasValue) {
                enemyFov.ComputeFov(enemy.X, enemy.Y, enemy.Awareness, true);
                if (enemyFov.IsInFov(player.X, player.Y)) {
                    game.Logger.Add(enemy.Name + " рвется в битву с " + player.Name + ".");
                    enemy.TurnsAlerted = 1;
                }
            }

            if (enemy.TurnsAlerted.HasValue) {
                mapManager.SetIsWalkable(enemy.X, enemy.Y, true);
                mapManager.SetIsWalkable(player.X, player.Y, true);

                PathFinder pathFinder = new PathFinder(mapManager.Map, 1d);
                Path path = null;

                try 
                {
                    path = pathFinder.ShortestPath(
                        mapManager.Map.GetCell(enemy.X, enemy.Y),
                        mapManager.Map.GetCell(player.X, player.Y)
                    );
                } 
                catch (PathNotFoundException) 
                {
                    game.Logger.Add(enemy.Name + " ждет своего хода.");
                }

                mapManager.SetIsWalkable(enemy.X, enemy.Y, false);
                mapManager.SetIsWalkable(player.X, player.Y, false);

                if (path != null) {
                    try
                    {
                        commandSystem.MoveEnemy(enemy, path.StepForward() as Cell);
                    }
                    catch (NoMoreStepsException)
                    {
                        game.Logger.Add(enemy.Name + " не может пройти.");
                    }
                }

                enemy.TurnsAlerted++;

                if (enemy.TurnsAlerted > 15) {
                    enemy.TurnsAlerted = null;
                }
            }

            return true;
        }
    }
}