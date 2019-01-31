using UnityEngine;
using UnityEngine.UI;

namespace BannersOfRogues.UI.Utils {
    public class StatBar : MonoBehaviour {
        [SerializeField] private Text symbolText;
        [SerializeField] private Text nameText;
        [SerializeField] private Slider healthBarSlider;
        [SerializeField] private Image fillImage;

        /// <summary>
        /// Рисует символ и имя монстра, также генерирует информацию о нем.
        /// </summary>
        public void SetSlider(int hp, int maxHP, string name, Color color, char symbol) {
            if (maxHP < 1) maxHP = 1;

            if (hp > maxHP) {
                hp = maxHP;
            } else if (hp < 0) {
                hp = 0;
            }

            healthBarSlider.value = (float)hp / (float)maxHP;

            if (nameText != null) {
                nameText.text = name;
            }

            if (symbolText != null) {
                string sym = "" + symbol;
                symbolText.text = sym;
            }

            nameText.color = Color.black;
            symbolText.color = color;
            fillImage.color = color;
        }
    }
}