using BannersOfRogues.Core;

namespace BannersOfRogues.Equipment
{
    public class FeetEquipment : Core.Equipment
    {
        public FeetEquipment(Game _game) : base(_game) {}

        public static FeetEquipment None(Game _game) {
            return new FeetEquipment(_game)
            {
                game = _game,
                Name = "None"
            };
        }

        public static FeetEquipment Leather(Game _game) {
            return new FeetEquipment(_game)
            {
                game = _game,
                Defense = 1,
                DefenseChance = 5,
                Name = "Leather"
            };
        }

        public static FeetEquipment Chain(Game _game) {
            return new FeetEquipment(_game)
            {
                game = _game,
                Defense = 1,
                DefenseChance = 10,
                Name = "Chain"
            };
        }

        public static FeetEquipment Plate(Game _game) {
            return new FeetEquipment(_game)
            {
                game = _game,
                Defense = 1,
                DefenseChance = 15,
                Name = "Plate"
            };
        }
    }
}