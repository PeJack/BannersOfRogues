using BannersOfRogues.Core;

namespace BannersOfRogues.Equipment
{
    public class HeadEquipment : Core.Equipment
    {
        public HeadEquipment(Game _game) : base(_game) { }

        public static HeadEquipment None(Game _game)
        {
            return new HeadEquipment(_game)
            {
                game = _game,
                Name = "None"
            };
        }

        public static HeadEquipment Leather(Game _game)
        {
            return new HeadEquipment(_game)
            {
                game = _game,
                Defense = 1,
                DefenseChance = 5,
                Name = "Leather"
            };
        }

        public static HeadEquipment Chain(Game _game)
        {
            return new HeadEquipment(_game)
            {
                game = _game,
                Defense = 1,
                DefenseChance = 10,
                Name = "Chain"
            };
        }

        public static HeadEquipment Plate(Game _game)
        {
            return new HeadEquipment(_game)
            {
                game = _game,
                Defense = 1,
                DefenseChance = 15,
                Name = "Plate"
            };
        }
    }
}