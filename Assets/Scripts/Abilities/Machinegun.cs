using BannersOfRogues.Core;
using BannersOfRogues.Interfaces;
using BannersOfRogues.World;
using BannersOfRogues.Characters;
using System;

using RogueSharp;

namespace BannersOfRogues.Abilities {
    public class Machinegun : Ability, ITargetable
    {
        private readonly int attack;
        private readonly int attackChance;
        private readonly float maxDistance;  


        public Machinegun(Game game, int attack, int attackChance) : base(game) {
            Name = "Автоматическая винтовка";
            Cooldown = 1;
            CurrCD = 0;
            this.attack = attack;
            this.attackChance = attackChance;
            this.maxDistance = 5.0f;
        }

        protected override bool PerformAbility() {
            return game.TargetingSystem.SelectLine(this);
        }

        public void SelectTarget(Point target) {

            MapManager mapManager = game.MapManager;
            Player player = game.Player;
            Point playerPoint = new Point {X = player.X, Y = player.Y};

            game.Logger.Add(string.Format("{0} стреляет из {1}", player.Name, Name));
            Actor dummy = new Actor(game)
            {   
                Attack = attack,
                AttackChance = attackChance,
                Name = Name
            };

            foreach (RogueSharp.Cell cell in mapManager.Map.GetCellsAlongLine(player.X, player.Y, target.X, target.Y)) {
                Point cellPoint = new Point {X = cell.X, Y = cell.Y};

                Enemy enemy = mapManager.GetEnemyAt(cell.X, cell.Y);
                if (enemy != null) {
                    game.CommandSystem.Attack(dummy, enemy);
                    return;
                }

                if (cell.IsWalkable) {
                    if (Point.Distance(playerPoint, cellPoint) > maxDistance) {
                        game.Logger.Add(string.Format("{0}(РДС) / {1}(макс дист). Пуля не долетела до цели.", Point.Distance(playerPoint, cellPoint), maxDistance));
                        return;
                    }
                    continue;
                }

                if (cell.X == player.X && cell.Y == player.Y) {
                    continue;
                }
                
                return;
            }
        }   
    }
}