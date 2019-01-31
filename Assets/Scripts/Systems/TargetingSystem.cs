using System.Collections.Generic;
using System.Linq;
using RogueSharp;

using BannersOfRogues.Interfaces;
using BannersOfRogues.Characters;
using BannersOfRogues.Systems.Utils;
using BannersOfRogues.Utils;
using BannersOfRogues.World;

namespace BannersOfRogues.Systems {
    public class TargetingSystem {
        private enum SelectionType
        {
            None = 0,
            Target = 1,
            Area = 2,
            Line = 3
        }

        public bool IsPlayerTargeting { get; private set; }

        private Point cursorPosition = null;
        private List<Point> selectableTargets = new List<Point>();
        private int currTargetIndex;
        private ITargetable targetable;
        private int area;
        private SelectionType selectionType;
        private Game game;

        public TargetingSystem(Game game) {
            this.game = game;
        }

        private void Clear() {
            cursorPosition = null;
            selectableTargets = new List<Point>();
            currTargetIndex = 0;
            area = 0;
            targetable = null;
            selectionType = SelectionType.None;
        }

        public bool SelectTarget(ITargetable targetable) {
            Clear();
            selectionType = SelectionType.Target;
            MapManager mapManager = game.MapManager;
            selectableTargets = mapManager.GetEnemyPositionInFOV().ToList();
            this.targetable = targetable;
            cursorPosition = selectableTargets.FirstOrDefault();

            if (cursorPosition == null) {
                StopTargeting();
                return false;
            }

            IsPlayerTargeting = true;
            return true;
        }

        public bool SelectArea(ITargetable targetable, int area = 0) {
            Clear();
            selectionType = SelectionType.Area;
            Player player = game.Player;
            cursorPosition = new Point { X = player.X, Y = player.Y };
            this.targetable = targetable;
            this.area = area;

            IsPlayerTargeting = true;
            return true;
        }

        public bool SelectLine(ITargetable targetable) {
            Clear();
            selectionType = SelectionType.Line;
            Player player = game.Player;
            cursorPosition = new Point { X = player.X, Y = player.Y };
            this.targetable = targetable;

            IsPlayerTargeting = true;
            return true;
        }

        public void StopTargeting() {
            IsPlayerTargeting = false;
            Clear();
        }

        public bool HandleKey(KeyboardKeys key) {
            if (selectionType == SelectionType.Target) {
                HandleSelectableTargeting(key);
            } else if (selectionType == SelectionType.Area) {
                HandlePositionTargeting(key);
            } else if (selectionType == SelectionType.Line) {
                HandlePositionTargeting(key);
            }

            if (key == KeyboardKeys.EnterKey) {
                targetable.SelectTarget(cursorPosition);
                StopTargeting();
                return true;
            }

            return false;
        }

        private void HandleSelectableTargeting(KeyboardKeys key) {
            if (key == KeyboardKeys.Right || key == KeyboardKeys.Down) {
                currTargetIndex++;

                if (currTargetIndex >= selectableTargets.Count) {
                    currTargetIndex = 0;
                }
            } else if (key == KeyboardKeys.Left || key == KeyboardKeys.Up) {
                currTargetIndex--;

                if (currTargetIndex < 0) {
                    currTargetIndex = selectableTargets.Count - 1;
                }
            }

            cursorPosition = selectableTargets[currTargetIndex];
        }

        private void HandlePositionTargeting(KeyboardKeys key) {
            int x = cursorPosition.X;
            int y = cursorPosition.Y;

            Map map = game.MapManager.Map;
            Player player = game.Player;        
            
            if (key == KeyboardKeys.Right) {
                x++;
            } else if (key == KeyboardKeys.Left) {
                x--;
            } else if (key == KeyboardKeys.Down) {
                y--;
            } else if (key == KeyboardKeys.Up) {
                y++;
            }

            if (map.IsInFov(x, y)) {
                cursorPosition.X = x;
                cursorPosition.Y = y;
            }
         }

        public void Draw() {
            if (IsPlayerTargeting) {
                Map map = game.MapManager.Map;
                Player player = game.Player;

                if (selectionType == SelectionType.Area) {
                    foreach (Cell cell in map.GetCellsInSquare(cursorPosition.X, cursorPosition.Y, area)) {
                        game.SetMapCellBackground(cell.X, cell.Y, Colors.DbSun);
                    }
                } else if (selectionType == SelectionType.Line) {
                    foreach (Cell cell in map.GetCellsAlongLine(player.X, player.Y, cursorPosition.X, cursorPosition.Y)) {
                        game.SetMapCellBackground(cell.X, cell.Y, Colors.DbSun);
                    }
                }

                game.SetMapCellBackground(cursorPosition.X, cursorPosition.Y, Colors.DbLight);
            } 
        }
 
    }
}