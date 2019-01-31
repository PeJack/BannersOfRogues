using UnityEngine;
using BannersOfRogues.Systems.Utils;

namespace BannersOfRogues.Systems {
    public class InputKeyboard : MonoBehaviour {
        private KeyboardKeys keyboardKeys;

        public KeyboardKeys LastKey {
            get {
                KeyboardKeys returnKey = keyboardKeys;
                keyboardKeys = KeyboardKeys.None;
                return returnKey;
            }
        }

        private void Update() {
            keyboardKeys = GetKeyboardKey();
        }

        private KeyboardKeys GetKeyboardKey() {
            if (Input.GetKeyUp(KeyCode.Keypad7) || Input.GetKey(KeyCode.Keypad7)) 
            {
                return KeyboardKeys.UpLeft;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
            {
                return KeyboardKeys.Up;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                return KeyboardKeys.Down;
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                return KeyboardKeys.Left;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                return KeyboardKeys.Right;
            }
            else if (Input.GetKey(KeyCode.Period))
            {
                return KeyboardKeys.StairsDown;
            }
            else if (Input.GetKeyUp(KeyCode.Comma))
            {
                return KeyboardKeys.StairsUp;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                return KeyboardKeys.Item1;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                return KeyboardKeys.Item2;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                return KeyboardKeys.Item3;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                return KeyboardKeys.Item4;
            }
            else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKey(KeyCode.Q))
            {
                return KeyboardKeys.QAbility;
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKey(KeyCode.W))
            {
                return KeyboardKeys.WAbility;
            }
            else if (Input.GetKeyUp(KeyCode.E) || Input.GetKey(KeyCode.W))
            {
                return KeyboardKeys.EAbility;
            }
            else if (Input.GetKeyUp(KeyCode.R) || Input.GetKey(KeyCode.R))
            {
                return KeyboardKeys.RAbility;
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                return KeyboardKeys.CloseGame;
            }
            else if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
            {
                return KeyboardKeys.EnterKey;
            }

            return KeyboardKeys.None;
        }
    }
}
