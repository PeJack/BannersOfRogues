using System;
using System.Collections.Generic;
using RogueSharp.Random;

using BannersOfRogues.Systems;
using BannersOfRogues.Systems.Utils;
using BannersOfRogues.Characters;
using BannersOfRogues.Generators;
using BannersOfRogues.World;
using BannersOfRogues.Utils;
using BannersOfRogues.UI;

using UnityEngine;

namespace BannersOfRogues {
    public class Game {
        public static IRandom Random { get; private set; }

        private UI.Main             screen;
        private InputKeyboard       inputControl;

        private static readonly int mapWidth  = 80;
        private static readonly int mapHeight = 48;
        private bool                needRender = true;
        private float               lastKeyPressTime;

        public Player               Player { get; set; }
        public MapManager           MapManager { get; private set; }
        public Systems.Logger       Logger { get; private set; }
        public SchedulingSystem     SchedulingSystem { get; private set; }
        public TargetingSystem      TargetingSystem { get; private set; }
        public CommandSystem        CommandSystem { get; private set; }

        public int                  mapLevel = 1;
        public float                KeyPressDelay = 0.05f;
        public Game(UI.Main screen) {
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);
            CommandSystem = new CommandSystem(this);
            Logger = new Systems.Logger(this);
            SchedulingSystem = new SchedulingSystem(this);
            TargetingSystem = new TargetingSystem(this);

            this.screen = screen;
            screen.UpdateRender += OnUpdate;
        }

        public void Run() {
            Logger.Add("Вы прибыли на уровень " + mapLevel);
            GenerateMap();
            screen.SetPlayer(Player);
            MapManager.UpdatePlayerFOV(Player);
            
            Draw();
        }

        public void SetMapCell(int x, int y, Color foreColor, Color backColor, char symbol, bool isExplored) {
            screen.UpdateMapCell(x, y, foreColor, backColor, symbol, isExplored);
        }

        public void SetMapCellBackground (int x, int y, Color backColor) {
            screen.UpdateBackgroundCell(x, y, backColor);
        }

        public void PrintLog(Queue<string> logs, Color color) {
            screen.PrintLog(logs, color);
        }

        public void DrawPlayerStats() {
            screen.DrawPlayerStats();
        }

        public void DrawInventory() {
            screen.DrawInventory();
        }

        public void DrawEnemyStats(Enemy enemy, int pos) {
            screen.DrawEnemyStats(enemy, pos);
        }

        public void ClearEnemyStats() {
            screen.ClearEnemyStats();
        }

        private void OnUpdate(object sender, UpdateEventArgs e) {
            if (Input.anyKeyDown || Time.time - lastKeyPressTime > KeyPressDelay) {
                lastKeyPressTime = Time.time;

                if (TargetingSystem.IsPlayerTargeting) {
                    KeyboardKeys key = screen.GetUserKey();
                    if (TargetingSystem.HandleKey(key)) {
                        CommandSystem.EndPlayerTurn();
                    }
                    needRender = true;
                } else if (CommandSystem.IsPlayerTurn) {
                    CheckKeyboard();
                } else {
                    CommandSystem.ActivateEnemies();
                    needRender = true;
                }
            }
            
            Logger.Draw();

            if (needRender) {
                Draw();
                needRender = false;
            }
        }

        private void GenerateMap() {
            MapGenerator generator = new MapGenerator(this, mapWidth, mapHeight, 20, 13, 7, "dungeon", mapLevel);
            MapManager = generator.Generate();
            screen.GenerateMap(MapManager.Map);
        }

        private void Draw() {              
            MapManager.Draw();
            Player.Draw(MapManager.Map);
            TargetingSystem.Draw();
            Player.DrawStats();         
        }

        private void CheckKeyboard() {
            bool didPlayerAct = false;

            KeyboardKeys key = screen.GetUserKey();
            if (CommandSystem.IsPlayerTurn) {
                switch (key) {
                    case KeyboardKeys.UpLeft:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.UpLeft);
                        break;
                    case KeyboardKeys.Up:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                        break;
                    case KeyboardKeys.UpRight:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.UpRight);
                        break;
                    case KeyboardKeys.Left:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                        break;
                    case KeyboardKeys.Right:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                        break;
                    case KeyboardKeys.DownLeft:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.DownLeft);
                        break;
                    case KeyboardKeys.Down:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                        break;
                    case KeyboardKeys.DownRight:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.DownRight);
                        break;
                    case KeyboardKeys.QAbility:
                        didPlayerAct = Player.QAbility.Perform();
                        break;
                    case KeyboardKeys.WAbility:
                        didPlayerAct = Player.WAbility.Perform();
                        break;
                    case KeyboardKeys.EAbility:
                        didPlayerAct = Player.EAbility.Perform();
                        break;
                    case KeyboardKeys.RAbility:
                        didPlayerAct = Player.RAbility.Perform();
                        break;
                    case KeyboardKeys.StairsDown:
                        if (MapManager.CanMoveToNextLevel()) {
                            MoveMapLevelDown();
                            didPlayerAct = true;
                        }
                        break;
                    case KeyboardKeys.CloseGame:
                        screen.CloseApp();
                        break;
                    default:
                        break;
                }
                if (didPlayerAct) {
                    needRender = true;
                    CommandSystem.EndPlayerTurn();
                }
            } 
 
        }

        private void MoveMapLevelDown() {
            screen.ClearMap();
            MapGenerator generator = new MapGenerator(this, mapWidth, mapHeight, 20, 13, 7, "cave", ++mapLevel);
            MapManager = generator.Generate();
            screen.GenerateMap(MapManager.Map);
            screen.SetPlayer(Player);
            MapManager.UpdatePlayerFOV(Player);
            Draw();

            Logger = new Systems.Logger(this);
            CommandSystem = new CommandSystem(this);
        }
    }
}