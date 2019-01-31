using RogueSharp;
using BannersOfRogues.Characters;
using BannersOfRogues.Characters.Enemies;
using BannersOfRogues.Systems;
using BannersOfRogues.Utils;

namespace BannersOfRogues.Generators {
    public static class ActorGenerator {
        private static Player _player = null;

        public static Enemy CreateEnemy(Game game, int level, Point pos) {
            GamePool<Enemy> enemyPool = new GamePool<Enemy>();
            enemyPool.Add(ScreamerM2.Create(game, level), 25);
            
            Enemy enemy = enemyPool.Get();
            enemy.X = pos.X;
            enemy.Y = pos.Y;

            return enemy;
        }

        public static Player CreatePlayer(Game game) {
            if (_player == null) {
                _player = new Player(game)
                {
                    Attack = 2,
                    AttackChance = 50,
                    Awareness = 15,
                    Color = Colors.Player,
                    Defense = 2,
                    DefenseChance = 40,
                    Gold = 0,
                    Health = 100,
                    MaxHealth = 100,
                    Name = "Киборг-командо",
                    Speed = 10,
                    Symbol = '@'
                };
            }

            return _player;
        }
    }


}