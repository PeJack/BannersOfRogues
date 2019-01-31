using System.Collections.Generic;
using UnityEngine;

namespace BannersOfRogues.Systems {
    public class Logger {
        private static readonly int maxLines = 9;
        private readonly Queue<string> lines;
        private Game game;

        public Logger(Game game) {
            this.game = game;
            lines = new Queue<string>();
        }

        public void Add(string message) {
            lines.Enqueue(message);

            if (lines.Count > maxLines) {
                lines.Dequeue();
            }
        }

        public void Draw() {
            game.PrintLog(lines, Color.white);
        }
    }
}