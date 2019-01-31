using RogueSharp;
using BannersOfRogues.Interfaces;
using BannersOfRogues.Utils;
using UnityEngine;

namespace BannersOfRogues.Core {
    public class Ability : IAbility {
        public string Name { get; protected set; }
        public int Cooldown { get; protected set; }
        /// <summary>
        /// Текущее время до перезарядки способности.
        /// </summary>
        public int CurrCD   { get; protected set; }

        public Color Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        protected Game game;

        public Ability(Game game) {
            Symbol = '*';
            Color = Color.yellow;

            this.game = game;
        }

        public bool Perform() {
            if (CurrCD > 0) {
                game.Logger.Add($"{Name} перезарядка.");
                return false;
            }

            CurrCD = Cooldown;

            return PerformAbility();
        }

        protected virtual bool PerformAbility() {
            return false;
        }

        public void Tick() {           
            if (CurrCD > 0) {
                CurrCD--;
            }
        }
    }
}