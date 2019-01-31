using System;
using System.Collections.Generic;

using BannersOfRogues.Utils;
using BannersOfRogues.Systems;
using BannersOfRogues.Systems.Utils;
using BannersOfRogues.World;
using BannersOfRogues.Characters;

using RogueSharp;

using UnityEngine;

namespace BannersOfRogues.UI {
    [System.Serializable]
    public class Main : MonoBehaviour {
        public event UpdateEventHandler UpdateRender;

        [SerializeField] private Stats  uiStats;
        [SerializeField] private Log    uiLog;
        [SerializeField] private Inventory uiInventory;
        [SerializeField] private InputKeyboard inputKeyboard;
        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private Renderer rendererPrefab;

        private Game game;
        private Renderer[,] mapObjects;

        private void Start() {
            uiStats       = GetComponent<UI.Stats>();
            uiLog         = GetComponent<UI.Log>();
            uiInventory   = GetComponent<UI.Inventory>();
            inputKeyboard = GetComponent<InputKeyboard>();

            game          = new Game(this);
            game.Run();
        }
        
        public void SetGameInstance(Game game) {
            this.game = game;
        }

        private void Update() {
            UpdateRender(this, new UpdateEventArgs(Time.time));
        }

        private bool IsInFov(Vector3 pos) {
            Vector3 pointOnScreen = Camera.main.WorldToScreenPoint(pos);

            if ((pointOnScreen.x < 0) || 
                (pointOnScreen.x > Screen.width) || 
                (pointOnScreen.y < 0) ||
                (pointOnScreen.y > Screen.height))
            {
                return false;
            }

            return true;
        }

        public KeyboardKeys GetUserKey() {
            return inputKeyboard.LastKey;
        }

        public void GenerateMap(Map map) {
            mapObjects = new Renderer[map.Width, map.Height];
        }

        public void ClearMap() {
            for (int x = 0; x < mapObjects.GetLength(0); x++) {
                for (int y = 0; y < mapObjects.GetLength(1); y++) {
                    if (mapObjects[x, y] != null) {
                        mapObjects[x, y].Active = false;
                        mapObjects[x, y].ReturnToPool();
                        mapObjects[x, y] = null;
                    }
                }
            }
        }

        public void UpdateMapCell(int x, int y, Color foreColor, Color backColor, char symbol, bool isExplored) {
            Renderer renderer;

            if (mapObjects[x, y] != null) {
                renderer = mapObjects[x, y];
            } else {
                renderer = null;
            }

            if (!IsInFov(new Vector3(x, y, 0))) {
                if (renderer != null) {
                    mapObjects[x, y] = null;
                    renderer.Active = false;
                    renderer.ReturnToPool();
                }
            } else {
                if (renderer == null) {
                    renderer = rendererPrefab.GetPooledInstance<Renderer>();
                    renderer.transform.position = new Vector3(x, y, 0);
                    renderer.IsAscii = true;
                    mapObjects[x, y] = renderer;
                }

                renderer.Active = isExplored;
                renderer.BackgroundColor = backColor;
                renderer.Text = symbol;
                renderer.TextColor = foreColor;
            }
        }

        public void UpdateBackgroundCell(int x, int y, Color backColor) {
            if (x >= 0 && x < mapObjects.GetLength(0) &&
                y >= 0 &&
                y < mapObjects.GetLength(1) &&
                mapObjects[x, y] != null)
            {
                mapObjects[x, y].BackgroundColor = backColor;
            }
        }

        public void PrintLog(Queue<string> logs, Color color) {
            uiLog.PrintLog(logs, color);
        }

        public void DrawInventory() {
            uiInventory.Draw(game);
        }

        public void DrawPlayerStats() {
            uiStats.DrawPlayerStats(game);
        }

        public void DrawEnemyStats(Enemy enemy, int pos) {
            uiStats.DrawEnemyStats(enemy, pos);
        }

        public void ClearEnemyStats() {
            uiStats.ClearEnemyStats();
        }

        public void SetPlayer(Player player) {
            playerCamera.Init(player);
        }

        public void CloseApp() {
            Application.Quit();
        }
    }
}