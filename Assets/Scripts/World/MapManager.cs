using RogueSharp;
using RogueSharp.MapCreation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using BannersOfRogues.Generators;
using BannersOfRogues.Characters;
using BannersOfRogues.World;
using BannersOfRogues.Utils;
using BannersOfRogues.Core;


namespace BannersOfRogues.World {
    
    public class MapManager {

        private Game game;

        public List<Rectangle> Rooms;
        public List<Door> Doors;

        public Stairs StairsUp;
        public Stairs StairsDown;
        public Map Map;
        private List<Enemy> enemies;

        public MapManager(Game game) {
            this.game           = game;
            this.game.SchedulingSystem.Clear();

            Rooms               = new List<Rectangle>();
            Doors               = new List<Door>();
            enemies             = new List<Enemy>();

        }

        public void AddPlayer(Player player) {
            game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            game.SchedulingSystem.Add(player);
        }

        public void AddEnemy(Enemy enemy) {
            enemies.Add(enemy);
            SetIsWalkable(enemy.X, enemy.Y, false);
            game.SchedulingSystem.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy) {
            enemies.Remove(enemy);
            SetIsWalkable(enemy.X, enemy.Y, true);
            game.SchedulingSystem.Remove(enemy);
        }

        public Enemy GetEnemyAt(int x, int y) {
            return enemies.FirstOrDefault(e => e.X == x && e.Y == y);
        }



        public void SetIsWalkable(int x, int y, bool isWalkable) {
            Cell cell = Map.GetCell(x, y) as Cell;
            Map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        private void SetSymbolForCell(Cell cell) {
            if (Map.IsInFov(cell.X, cell.Y)) {
                if (cell.IsWalkable) {
                    game.SetMapCell(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.', cell.IsExplored);
                } else {
                    game.SetMapCell(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#', cell.IsExplored);
                }
            } else {
                if (cell.IsWalkable) {
                    game.SetMapCell(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.', cell.IsExplored);
                } else {
                    game.SetMapCell(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#', cell.IsExplored);
                }
            }
        }

        public Door GetDoor(int x, int y) {
            return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        private void OpenDoor(Actor actor, int x, int y) {
            Door door = GetDoor(x, y);

            if (door != null && !door.IsOpen) {
                door.IsOpen = true;
                var cell = Map.GetCell(x, y);

                Map.SetCellProperties(x, y, true, cell.IsWalkable, cell.IsExplored);

                game.Logger.Add(actor.Name + " открыл дверь.");
            }
        }

        public void Draw() {
            foreach (Cell cell in Map.GetAllCells()) {
                SetSymbolForCell(cell);
            }

            foreach (Door door in Doors) {
                door.Draw(Map);
            }

            StairsUp.Draw(Map);
            StairsDown.Draw(Map);

            int i = -1;

            foreach (Enemy enemy in enemies) {
                enemy.Draw(Map);

                if (Map.IsInFov(enemy.X, enemy.Y)) {
                    i++;
                    enemy.DrawStats(i);
                }
            } 

            if (i == -1) {
                game.ClearEnemyStats();
            }
        }

        public void UpdatePlayerFOV(Player player) {
            Map.ComputeFov(player.X, player.Y, player.Awareness, true);
            foreach (Cell cell in Map.GetAllCells()) {
                if (Map.IsInFov(cell.X, cell.Y)) {
                    Map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        public bool SetActorPosition(Actor actor, int x, int y) {
            if (Map.GetCell(x, y).IsWalkable) {
                SetIsWalkable(actor.X, actor.Y, true);
                actor.X = x;
                actor.Y = y;
                SetIsWalkable(actor.X, actor.Y, false);

                OpenDoor(actor, x, y);

                if (actor is Player) {
                    UpdatePlayerFOV(actor as Player);
                }
                return true;
            }

            return false;
        }

        public bool CanMoveToNextLevel() {
            Player player = game.Player;
            return StairsDown.X == player.X && StairsDown.Y == player.Y;
        }

        public IEnumerable<Point> GetEnemyPosition() {
            return enemies.Select(e => new Point { X = e.X, Y = e.Y });
        }

        public IEnumerable<Point> GetEnemyPositionInFOV() {
            return enemies.Where(enemy => Map.IsInFov(enemy.X, enemy.Y))
                .Select(e => new Point { X = e.X, Y = e.Y });
        }

        public Point GetRandomPosition() {
            int roomNum = Game.Random.Next(0, Rooms.Count - 1);
            Rectangle randomRoom = Rooms[roomNum];

            if (!DoesRoomHaveWalkableSpace(randomRoom)) {
                GetRandomPosition();
            }

            return GetRandomPositionInRoom(randomRoom);
        }

        public bool DoesRoomHaveWalkableSpace(Rectangle room) {
            for (int x = 1; x <= room.Width - 2; x++) {
                for (int y = 1; y <= room.Height - 2; y++) {
                    if (Map.IsWalkable(x + room.X, y + room.Y)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public Point GetRandomPositionInRoom(Rectangle room) {
            int x = Game.Random.Next(1, room.Width - 2) + room.X;
            int y = Game.Random.Next(1, room.Height - 2) + room.Y;
            
            if (!Map.IsWalkable(x, y)) {
                GetRandomPositionInRoom(room);
            }
            return new Point(x, y);
        }


        public Point GetRandomWalkablePositionInRoom(Rectangle room) {
            if (DoesRoomHaveWalkableSpace(room)) {
                for (int i = 0; i < 100; i++) {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    if (Map.IsWalkable(x, y)) {
                        return new Point(x, y);
                    }
                }
            }

            return Point.Zero;
        }
    }        
}