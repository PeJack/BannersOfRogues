using BannersOfRogues.Core;
using BannersOfRogues.Abilities;
using BannersOfRogues.Interfaces;

namespace BannersOfRogues.Characters {
    public class Player : Actor {
        public IAbility QAbility { get; set; }
        public IAbility WAbility { get; set; }
        public IAbility EAbility { get; set; } 
        public IAbility RAbility { get; set; }

        public Player(Game game) : base(game) {
            QAbility = new Abilities.Shotgun(game, 2, 100, 1);
            WAbility = new Abilities.Machinegun(game, 2, 100);
            EAbility = new Abilities.None(game);
            RAbility = new Abilities.None(game);
        }

        public bool AddAbility(IAbility ability) {
            if (QAbility is Abilities.None) {
                QAbility = ability;
            } else if (WAbility is Abilities.None) {
                WAbility = ability;
            } else if (EAbility is Abilities.None) {
                EAbility = ability;
            } else if (RAbility is Abilities.None) {
                RAbility = ability;
            } else {
                return false;
            }

            return true;
        }

        public void Tick() {
            QAbility?.Tick();
            WAbility?.Tick();
            EAbility?.Tick();
            RAbility?.Tick();
        }

        public void DrawStats() {
            game.DrawPlayerStats();
            game.DrawInventory();
        }
    }
}