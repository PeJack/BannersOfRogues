using UnityEngine;
using UnityEngine.UI;
using BannersOfRogues.Equipment;
using BannersOfRogues.Abilities;
using BannersOfRogues.Utils;

namespace BannersOfRogues.UI {
    public class Inventory : MonoBehaviour {
        // eq - equipment
        [SerializeField] private Text eqHeadText;
        [SerializeField] private Text eqHandsText;
        [SerializeField] private Text eqFeetText;
        [SerializeField] private Text eqBodyText;

        // ab - ability        
        [SerializeField] private Text abQText;
        [SerializeField] private Text abWText;
        [SerializeField] private Text abEText;
        [SerializeField] private Text abRText;

        public void Draw(Game game) {
            if (game == null) return;

            eqHeadText.text = game.Player.Head.Name;
            eqHandsText.text = game.Player.Hand.Name;
            eqBodyText.text = game.Player.Body.Name;
            eqFeetText.text = game.Player.Feet.Name;

            abQText.text = game.Player.QAbility.Name;
            abWText.text = game.Player.WAbility.Name;
            abEText.text = game.Player.EAbility.Name;
            abRText.text = game.Player.RAbility.Name;

            if (game.Player.Head == HeadEquipment.None(game)) {
                eqHeadText.color = Colors.DbOldStone;
            } else {
                eqHeadText.color = Colors.DbLight;
            }

            if (game.Player.Hand == HandEquipment.None(game)) {
                eqHandsText.color = Colors.DbOldStone;
            } else {
                eqHandsText.color = Colors.DbLight;
            }

            if (game.Player.Body == BodyEquipment.None(game)) {
                eqBodyText.color = Colors.DbOldStone;
            } else {
                eqBodyText.color = Colors.DbLight;
            }

            if (game.Player.Feet == FeetEquipment.None(game)) {
                eqFeetText.color = Colors.DbOldStone;
            } else {
                eqFeetText.color = Colors.DbLight;
            }

            if (game.Player.QAbility is Abilities.None) {
                abQText.color = Colors.DbOldStone;
            } else {
                abQText.color = Colors.DbLight;
            }

            if (game.Player.WAbility is Abilities.None) {
                abWText.color = Colors.DbOldStone;
            } else {
                abWText.color = Colors.DbLight;
            }
            
            if (game.Player.EAbility is Abilities.None) {
                abEText.color = Colors.DbOldStone;
            } else {
                abEText.color = Colors.DbLight;
            }

            if (game.Player.RAbility is Abilities.None) {
                abRText.color = Colors.DbOldStone;
            } else {
                abRText.color = Colors.DbLight;
            }                      
        }
    }
}