using BannersOfRogues.Core;

namespace BannersOfRogues.Equipment {
    public class BodyEquipment : Core.Equipment {
        public BodyEquipment(Game _game) : base(_game) {}

        public static BodyEquipment None(Game _game) {
            return new BodyEquipment(_game)
            {
                game = _game,
                Name = "None"
            };
        }

        public static BodyEquipment Leather(Game _game)
        {
            return new BodyEquipment(_game)
            {
                game = _game,
                Defense = 1,
                DefenseChance = 10,
                Name = "Leather"
            };
        }

        public static BodyEquipment Chain(Game _game)
        {
            return new BodyEquipment(_game)
            {
                game = _game,
                Defense = 2,
                DefenseChance = 5,
                Name = "Chain"
            };
        }

        public static BodyEquipment Plate(Game _game)
        {
            return new BodyEquipment(_game)
            {
                game = _game,
                Defense = 2,
                DefenseChance = 10,
                Name = "Plate"
            };
        }
    }
}