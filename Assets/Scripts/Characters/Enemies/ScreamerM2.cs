using BannersOfRogues.Characters;
using RogueSharp.DiceNotation;
using BannersOfRogues.Utils;
using BannersOfRogues.Systems;
using BannersOfRogues.Characters.Behaviours;

namespace BannersOfRogues.Characters.Enemies {
    public class ScreamerM2 : Enemy {
        public ScreamerM2 (Game game) : base(game) {}

        public static ScreamerM2 Create(Game game, int level) {
            int hp = Dice.Roll("2D5");

            return new ScreamerM2(game) {
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.KoboldColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("5D5"),
                Health = hp,
                MaxHealth = hp,
                Name = "Крикун М2",
                Speed = 14,
                Symbol = '2'
            };
        }

        // public override void PerformAction(CommandSystem commandSystem) {
        //     var wanderBehaviour = new Wander();
        //     wanderBehaviour.Act(this, commandSystem, game);
        // }
    }
}