using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BannersOfRogues.UI {
    public class Log : MonoBehaviour {
        [SerializeField] private Text logText;

        public void PrintLog(Queue<string> logs, Color color) {
            string _text = "";
            string[] lines = logs.ToArray();

            for (int i = 0; i < lines.Length; i++) {
                _text += lines[i];
                if (i < lines.Length - 1) {
                    _text += Environment.NewLine;
                }
            }

            logText.color = color;
            logText.text = _text;
        }    
    }
}