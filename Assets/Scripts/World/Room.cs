using RogueSharp;
using BannersOfRogues.Interfaces;
using UnityEngine;
using BannersOfRogues.Utils;

namespace BannersOfRogues.World {
    public class Door : IDrawable {
        public bool IsOpen { get; set; }
        public Color Color { get; set; }
        public Color BackgroundColor { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        private Game game;

        public Door(Game game) {
            Symbol          = '+';
            Color           = Colors.Door;
            BackgroundColor = Colors.DoorBackground;
            this.game       = game;
        }

        public void Draw(IMap map) {
            if (!map.GetCell(X, Y).IsExplored) { return; }

            Symbol = IsOpen ? '-' : '+';

            if (map.IsInFov(X, Y)) {
                Color = Colors.DoorFov;
                BackgroundColor = Colors.DoorBackgroundFov;
            } else {
                Color = Colors.Door;
                BackgroundColor = Colors.DoorBackground;
            }

            game.SetMapCell(X, Y, Color, BackgroundColor, Symbol, map.GetCell(X, Y).IsExplored);
        }
    }
}