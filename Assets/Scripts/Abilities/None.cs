using BannersOfRogues.Core;

namespace BannersOfRogues.Abilities {
    public class None : Ability {
        public None(Game game) : base(game) {
            Name = "None";
            Cooldown = 0;
            CurrCD = 0;
        }

        protected override bool PerformAbility() {
            game.Logger.Add("Нет способности или режима оружия.");
            return false;
        }
    }
}