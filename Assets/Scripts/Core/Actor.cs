using BannersOfRogues.Interfaces;
using BannersOfRogues.Utils;
using BannersOfRogues.Equipment;
using UnityEngine;
using RogueSharp;

namespace BannersOfRogues.Core {
    public class Actor : IActor, IDrawable, IScheduleable {
        
        // IActor
        private int attack;
        private int attackChance;
        private int awareness;
        private int defense;
        private int defenseChance;
        private int gold;
        private int health;
        private int maxHealth;
        private string name;
        private int speed;

        public  HeadEquipment Head { get; set; }
        public  BodyEquipment Body { get; set; }
        public  HandEquipment Hand { get; set; }
        public  FeetEquipment Feet { get; set; }

        // IDrawable
        public Color Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        // IScheduleable
        public int Time { get {return Speed;} }

        protected Game game;

        public Actor(Game game) {
            this.game = game;

            Head = HeadEquipment.None(game);
            Body = BodyEquipment.None(game);
            Hand = HandEquipment.None(game);
            Feet = FeetEquipment.None(game);
        }

        public int Attack {
            get 
            { 
                return attack;
            }
            set 
            {
                attack = value;
            }
        }

        public int AttackChance {
            get 
            { 
                return attackChance;
            }
            set 
            {
                attackChance = value;
            }
        }

        public int Awareness {
            get 
            { 
                return awareness;
            }
            set 
            {
                awareness = value;
            }
        }

        public int Defense {
            get 
            { 
                return defense;
            }
            set 
            {
                defense = value;
            }
        }

        public int DefenseChance {
            get 
            { 
                return defenseChance;
            }
            set 
            {
                defenseChance = value;
            }
        }

        public int Gold {
            get 
            { 
                return gold;
            }
            set 
            {
                gold = value;
            }
        }

        public int Health {
            get 
            { 
                return health;
            }
            set 
            {
                health = value;
            }
        }

        public int MaxHealth {
            get 
            { 
                return maxHealth;
            }
            set 
            {
                maxHealth = value;
            }
        }

        public string Name {
            get 
            { 
                return name;
            }
            set 
            {
                name = value;
            }
        }        

        public int Speed {
            get 
            { 
                return speed;
            }
            set 
            {
                speed = value;
            }
        }

        public void Draw(IMap map) {
            if (!map.GetCell(X, Y).IsExplored) {
                return;
            }

            if (map.IsInFov(X, Y)) {
                game.SetMapCell(X, Y, Color, Colors.FloorBackgroundFov, Symbol, map.GetCell(X, Y).IsExplored);
            } else {
                game.SetMapCell(X, Y, Colors.Floor, Colors.FloorBackground, '.', map.GetCell(X, Y).IsExplored);
            }
        }
    }
}