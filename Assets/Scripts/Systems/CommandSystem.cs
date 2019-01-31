
using System.Text;

using RogueSharp;
using RogueSharp.DiceNotation;

using BannersOfRogues.Systems.Utils;
using BannersOfRogues.Interfaces;
using BannersOfRogues.Characters;
using BannersOfRogues.Equipment;
using BannersOfRogues.Core;

namespace BannersOfRogues.Systems {
    public class CommandSystem {
        private Game game;
        public bool IsPlayerTurn { get; set; }

        public CommandSystem(Game game) {
            this.game = game;
        }

        public bool MovePlayer(Direction dir) {
            int x = game.Player.X;
            int y = game.Player.Y;

            switch (dir) 
            {
                case Direction.Up:
                {
                    y = game.Player.Y + 1;
                    break;
                }
                case Direction.Down:
                {
                    y = game.Player.Y - 1;
                    break;
                }
                case Direction.Left:
                {
                    x = game.Player.X - 1;
                    break;
                }
                case Direction.Right:
                {
                    x = game.Player.X + 1;
                    break;
                }
                case Direction.UpLeft:
                {
                    x = game.Player.X - 1;
                    y = game.Player.Y + 1;
                    break;
                }
                case Direction.UpRight:
                {
                    x = game.Player.X + 1;
                    y = game.Player.Y + 1;
                    break;
                }
                case Direction.DownLeft:
                {
                    x = game.Player.X - 1;
                    y = game.Player.Y - 1;
                    break;
                }
                case Direction.DownRight:
                {
                    x = game.Player.X + 1;
                    y = game.Player.Y - 1;
                    break;
                }
                default:
                {
                    return false;
                }
            }

            if (game.MapManager.SetActorPosition(game.Player, x, y)) {
                return true;
            }

            Enemy enemy = game.MapManager.GetEnemyAt(x, y);

            if (enemy != null) {
                Attack(game.Player, enemy);
                return true;
            }

            return false;
        }

        public void EndPlayerTurn() {
            game.Player.Tick();
            IsPlayerTurn = false;
        } 

        public void ActivateEnemies() {
            IScheduleable scheduleable = game.SchedulingSystem.Get();

            if (scheduleable is Player) {
                IsPlayerTurn = true;
                game.SchedulingSystem.Add(game.Player);
            } else {
                Enemy enemy = scheduleable as Enemy;

                if (enemy != null) {
                    enemy.PerformAction(this);
                    game.SchedulingSystem.Add(enemy);
                }

                ActivateEnemies();
            }
        }

        public void MoveEnemy(Enemy enemy, Cell cell) {
            if (!game.MapManager.SetActorPosition(enemy, cell.X, cell.Y)) {
                if (game.Player.X == cell.X && game.Player.Y == cell.Y) {
                    Attack(enemy, game.Player);
                }
            }
        }

        public void Attack(Actor attacker, Actor defender) {
            StringBuilder attackMsg = new StringBuilder();
            StringBuilder defenseMsg = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMsg);
            int blocks = ResolveDefense(defender, hits, attackMsg, defenseMsg);

            game.Logger.Add(attackMsg.ToString());
            if (!string.IsNullOrEmpty(defenseMsg.ToString())) {
                game.Logger.Add(defenseMsg.ToString());
            }

            int damage = hits - blocks;
            ResolveDamage(defender, damage);
        }

        private int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMsg) {
            int hits = 0;

            attackMsg.AppendFormat("{0} атакует {1} с рейтингом: ", attacker.Name, defender.Name);

            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackRoll = attackDice.Roll();

            foreach (TermResult termResult in attackRoll.Results) {
                attackMsg.Append(termResult.Value + ", ");

                if (termResult.Value >= 100 - attacker.AttackChance) {
                    hits++;
                }
            }

            return hits;
        }

        private int ResolveDefense(Actor defender, int hits, StringBuilder attackMsg, StringBuilder defenseMsg) {
            int blocks = 0;

            if (hits > 0) {
                attackMsg.AppendFormat("Атака с рейтингом {0} попадания.", hits);
                defenseMsg.AppendFormat("   {0} защищается с рейтингом: ", defender.Name);

                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                foreach (TermResult termResult in defenseRoll.Results) {
                    defenseMsg.Append(termResult.Value + ", ");

                    if (termResult.Value >= 100 - defender.DefenseChance) {
                        blocks++;
                    }
                    defenseMsg.AppendFormat("{0}.", blocks);   
                }
            } else {
                attackMsg.Append("и полностью промахивается.");
            }

            return blocks;
        }

        private void ResolveDamage(Actor defender, int damage) {
            if (damage > 0) {
                defender.Health -= damage;

                game.Logger.Add(defender.Name + " было нанесено " + damage + " урона.");

                if (defender.Health <= 0) {
                    ResolveDeath(defender);
                }
            } else {
                game.Logger.Add(defender.Name + " заблокировал весь урон.");
            }
        }

        private void ResolveDeath(Actor defender) {
            if (defender is Player) {
                game.Logger.Add(defender.Name + " погиб от полученного урона.");
            } else if (defender is Enemy) {
                game.MapManager.RemoveEnemy((Enemy)defender);

                game.Logger.Add(defender.Name + " погиб от полученного урона.");
            }
         }
    }
}