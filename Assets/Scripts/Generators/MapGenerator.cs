using System;
using System.Linq;
using System.Collections.Generic;

using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharp.MapCreation;

using BannersOfRogues.World;
using BannersOfRogues.Characters;

namespace BannersOfRogues.Generators {
    public class MapGenerator {
        private readonly int width;
        private readonly int height;
        private readonly int maxRooms;
        private readonly int roomMaxSize;
        private readonly int roomMinSize;
        private readonly string mapType;
        private readonly int mapLevel;
        private readonly Game game;

        private MapManager mapManager;

        public MapGenerator(Game game, int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, string mapType, int mapLevel) {
            this.game           = game;
            this.width          = width;
            this.height         = height;
            this.maxRooms       = maxRooms;
            this.roomMaxSize    = roomMaxSize;
            this.roomMinSize    = roomMinSize;
            this.game           = game;
            this.mapType        = mapType;
            this.mapLevel       = mapLevel;
        } 

        public MapManager Generate() {
            mapManager = new MapManager(game);

            switch(mapType) {
                case "cave": 
                    mapManager.Map = Map.Create(new CaveMapCreationStrategy<Map>(width, height, 40, 5, 3));
                    break;
                case "dungeon":
                    mapManager.Map = Map.Create(new RandomRoomsMapCreationStrategy<Map>(width, height, 0, roomMaxSize, roomMinSize));
                    break;  
                default:
                    break;                  
            }

            for (int r = 0; r < maxRooms; r++) {
                int roomWidth = Game.Random.Next(roomMinSize, roomMaxSize);
                int roomHeight = Game.Random.Next(roomMinSize,roomMaxSize);
                int roomXPosition = Game.Random.Next(0, width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, height - roomHeight - 1);

                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);
                bool newRoomIntersects = mapManager.Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects) {
                    mapManager.Rooms.Add(newRoom);
                }
            }

            for (int r = 1; r < mapManager.Rooms.Count; r++) {
                int prevRoomCenterX = mapManager.Rooms[r - 1].Center.X;
                int prevRoomCenterY = mapManager.Rooms[r - 1].Center.Y;
                int currRoomCenterX = mapManager.Rooms[r].Center.X;
                int currRoomCenterY = mapManager.Rooms[r].Center.Y;

                if (Game.Random.Next(1, 2) == 1) {
                    for (int x = Math.Min(prevRoomCenterX, currRoomCenterX); x <= Math.Max(prevRoomCenterX, currRoomCenterX); x++) {
                        mapManager.Map.SetCellProperties(x, prevRoomCenterY, true, true);
                    }

                    for (int y = Math.Min(prevRoomCenterY, currRoomCenterY); y <= Math.Max(prevRoomCenterY, currRoomCenterY); y++) {
                        mapManager.Map.SetCellProperties(currRoomCenterX, y, true, true);
                    }
                } else {
                    for (int y = Math.Min(prevRoomCenterY, currRoomCenterY); y <= Math.Max(prevRoomCenterY, currRoomCenterY); y++) {
                        mapManager.Map.SetCellProperties(prevRoomCenterX, y, true, true);
                    }

                    for (int x = Math.Min(prevRoomCenterX, currRoomCenterX); x <= Math.Max(prevRoomCenterX, currRoomCenterX); x++) {
                        mapManager.Map.SetCellProperties(x, currRoomCenterY, true, true);
                    }
                }           
            }


            foreach (Rectangle room in mapManager.Rooms) {
                CreateRoom(room);
                CreateDoors(room);
            }

            List<ICell> doorCells = new List<ICell>();
            foreach (Door door in mapManager.Doors) {
                doorCells.Add(mapManager.Map.GetCell(door.X, door.Y));
            }

            CreateStairs();
            PlacePlayer();
            PlaceEnemies();

            return mapManager;
        }

        private void CreateRoom(Rectangle room) {
            for (int x = room.Left + 1; x < room.Right; x++) {
                for (int y = room.Top + 1; y < room.Bottom; y++) {
                    mapManager.Map.SetCellProperties(x, y, true, true, false);
                }
            }
        }

        private void CreateDoors(Rectangle room) {
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            List<ICell> borderCells = new List<ICell>();
            borderCells.AddRange(mapManager.Map.GetCellsAlongLine(xMin, yMin, xMax, yMin));
            borderCells.AddRange(mapManager.Map.GetCellsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange(mapManager.Map.GetCellsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange(mapManager.Map.GetCellsAlongLine(xMax, yMin, xMax, yMax));


            foreach (Cell cell in borderCells) {
                if (IsPotentialDoor(cell)) {
                    mapManager.Map.SetCellProperties(cell.X, cell.Y, false, true);
                    mapManager.Doors.Add(new Door(game) {
                        X = cell.X,
                        Y = cell.Y,
                        IsOpen = false
                    });
                }
            }                
        }

        private bool IsPotentialDoor(Cell cell) {
            if (!cell.IsWalkable) { return false; }

            Cell right  = (Cell)mapManager.Map.GetCell(cell.X + 1, cell.Y);
            Cell left   = (Cell)mapManager.Map.GetCell(cell.X - 1, cell.Y);
            Cell top    = (Cell)mapManager.Map.GetCell(cell.X, cell.Y - 1);
            Cell bottom = (Cell)mapManager.Map.GetCell(cell.X, cell.Y + 1);

            if (mapManager.GetDoor(cell.X, cell.Y) != null ||
                mapManager.GetDoor(right.X, right.Y) != null ||
                mapManager.GetDoor(left.X, left.Y) != null ||
                mapManager.GetDoor(top.X, top.Y) != null ||
                mapManager.GetDoor(bottom.X, bottom.Y) != null)
            {
                return false;
            }

            if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable) {
                return true;
            }

            if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable) {
                return true;
            }

            return false;
        }

        private void CreateStairs() {
            mapManager.StairsUp = new Stairs(game) {
                X = mapManager.Rooms.First().Center.X + 1,
                Y = mapManager.Rooms.First().Center.Y,
                IsUp = true
            };

            mapManager.StairsDown = new Stairs(game) {
                X = mapManager.Rooms.Last().Center.X,
                Y = mapManager.Rooms.Last().Center.Y,
                IsUp = false
            };
        }

        private void PlacePlayer() {
            Player player = ActorGenerator.CreatePlayer(game);

            player.X = mapManager.Rooms[0].Center.X;
            player.Y = mapManager.Rooms[0].Center.Y;

            mapManager.AddPlayer(player);
        }

        private void PlaceEnemies() {
            // for (int j = 1; j < 2; j++) {
            //     var numberOfEnemies = 1;

            //     for (int i = 0; i < numberOfEnemies; i++) {
            //         if (map.DoesRoomHaveWalkableSpace(map.Rooms[j])) {
            //             Point randomRoomPos = map.GetRandomPositionInRoom(map.Rooms[j]);
            //             if (randomRoomPos != null) {
            //                 map.AddEnemy(ActorGenerator.CreateEnemy(game, level, map.GetRandomPositionInRoom(map.Rooms[j])));
            //             }
            //         }
            //     }
            // }

            for (int j = 1; j < mapManager.Rooms.Count; j++) {
                if (Dice.Roll("1D10") < 7) {
                    var numberOfEnemies = 1;

                    for (int i = 0; i < numberOfEnemies; i++) {
                        if (mapManager.DoesRoomHaveWalkableSpace(mapManager.Rooms[j])) {
                            Point randomRoomPos = mapManager.GetRandomPositionInRoom(mapManager.Rooms[j]);
                            if (randomRoomPos != null) {
                                mapManager.AddEnemy(ActorGenerator.CreateEnemy(game, mapLevel, mapManager.GetRandomPositionInRoom(mapManager.Rooms[j])));
                            }
                        }
                    }
                }
            }
        }
    }
}