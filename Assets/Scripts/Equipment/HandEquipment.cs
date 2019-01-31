using BannersOfRogues.Core;

namespace BannersOfRogues.Equipment
{
    public class HandEquipment : Core.Equipment
    {
        public HandEquipment(Game _game) : base(_game) { }

        public static HandEquipment None(Game _game)
        {
            return new HandEquipment(_game)
            {
                game = _game,
                Name = "None"
            };
        }

        public static HandEquipment Dagger(Game _game)
        {
            return new HandEquipment(_game)
            {
                game = _game,
                Attack = 1,
                AttackChance = 10,
                Name = "Dagger",
                Speed = -2
            };
        }

        public static HandEquipment Sword(Game _game)
        {
            return new HandEquipment(_game)
            {
                game = _game,
                Attack = 1,
                AttackChance = 20,
                Name = "Sword",
                Speed = 0
            };
        }

        public static HandEquipment Axe(Game _game)
        {
            return new HandEquipment(_game)
            {
                game = _game,
                Attack = 2,
                AttackChance = 15,
                Name = "Axe",
                Speed = 1
            };
        }

        public static HandEquipment TwoHandedSword(Game _game)
        {
            return new HandEquipment(_game)
            {
                game = _game,
                Attack = 3,
                AttackChance = 30,
                Name = "2H Sword",
                Speed = 3
            };
        }
    }
}