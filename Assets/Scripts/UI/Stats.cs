using UnityEngine;
using UnityEngine.UI;

using BannersOfRogues.Utils;
using BannersOfRogues.Characters;
using BannersOfRogues.UI.Utils;

namespace BannersOfRogues.UI {
    public class Stats : MonoBehaviour {
        [SerializeField] private Text nameField;
        [SerializeField] private Text healthField;
        [SerializeField] private Text attackField;
        [SerializeField] private Text defenseField;
        // [SerializeField] private Text goldField;
        [SerializeField] private Text mapLevelField;
        [SerializeField] private VerticalLayoutGroup enemyGroup;
        [SerializeField] private GameObject statBarPrefab;

        public void DrawPlayerStats(Game game) {
            if (game == null) return;

            nameField.color = healthField.color = attackField.color = defenseField.color = mapLevelField.color = Colors.Text;
            // goldField.color = Colors.Gold;

            nameField.text = "Имя:      " + game.Player.Name;
            healthField.text = "Здоровье:  " + game.Player.Health + "/" + game.Player.MaxHealth;
            attackField.text = "Атака:    " + game.Player.Attack + " (ШП " + game.Player.AttackChance + "%)";
            defenseField.text = "Защита:   " + game.Player.Defense + " (ШЗ " + game.Player.DefenseChance + "%";
            // goldField.text = "Золото:    " + game.Player.Gold;
            mapLevelField.text = "Уровень:   " + game.mapLevel; 
        }

        public void DrawEnemyStats(Enemy enemy, int pos) {
            if (pos == 0) {
                ClearEnemyStats();
            }

            GameObject obj = Instantiate(statBarPrefab);
            obj.transform.SetParent(enemyGroup.transform, false);

            StatBar bar = obj.GetComponent<StatBar>();

            bar.SetSlider(enemy.Health, enemy.MaxHealth, enemy.Name, enemy.Color, enemy.Symbol);
        }

        public void ClearEnemyStats() {
            foreach (Transform child in enemyGroup.transform) {
                Destroy(child.gameObject);
            }
        }
    }
}