using RogueSharp;
using UnityEngine;

using BannersOfRogues.Interfaces;
using BannersOfRogues.Utils;

namespace BannersOfRogues.Core {
    public class Equipment : IEquipment, ITreasure, IDrawable {
        // IEquipment
        public int Attack { get; set; }
        public int AttackChance { get; set; }
        public int Awareness { get; set; }
        public int Defense { get; set; }
        public int DefenseChance { get; set; }
        public int Gold { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }

        // IDrawable
        public Color Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        protected Game game;

        public Equipment(Game game) {
            Symbol = ']';
            Color = Color.yellow;
            this.game = game;
        }

        protected bool Equals(Equipment anotherEquip) {
            return Attack        == anotherEquip.Attack &&
                   AttackChance  == anotherEquip.AttackChance &&
                   Awareness     == anotherEquip.Awareness &&
                   Defense       == anotherEquip.Defense &&
                   DefenseChance == anotherEquip.DefenseChance &&
                   Gold          == anotherEquip.Gold &&
                   Health        == anotherEquip.Health &&
                   MaxHealth     == anotherEquip.MaxHealth &&
                   Speed         == anotherEquip.Speed &&
                   Name          == anotherEquip.Name;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            
            if (ReferenceEquals(this, obj)) {
                return true;
            }

            if (obj.GetType() != this.GetType()) {
                return false;
            }

            return Equals((Equipment)obj);
        }

        public override int GetHashCode() {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ Attack;
                hashCode = (hashCode * 397) ^ AttackChance;
                hashCode = (hashCode * 397) ^ Awareness;
                hashCode = (hashCode * 397) ^ Defense;
                hashCode = (hashCode * 397) ^ DefenseChance;
                hashCode = (hashCode * 397) ^ Gold;
                hashCode = (hashCode * 397) ^ Health;
                hashCode = (hashCode * 397) ^ MaxHealth;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Speed;
                return hashCode;
            }
        }

        public static bool operator ==(Equipment left, Equipment right) {
            return Equals(left, right);
        }

        public static bool operator !=(Equipment left, Equipment right) {
            return !Equals(left, right);
        }

        public bool PickUp(IActor actor) {

            return false;
        }

        public void Draw(IMap map) {
            if (!map.IsExplored(X, Y)) {
                return;
            }
            
            if (map.IsInFov(X, Y)) {
                game.SetMapCell(X, Y, Color, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);
            } else {
                game.SetMapCell(X, Y, Colors.YellowGray, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);                
            }
        }
    }
}