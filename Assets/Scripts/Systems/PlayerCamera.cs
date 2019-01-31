using UnityEngine;
using BannersOfRogues.Characters;

namespace BannersOfRogues.Systems {
    public class PlayerCamera : MonoBehaviour {
        [SerializeField]
        private Player player;
        private float xPos, yPos;

        private void LateUpdate() {
            if (player != null) {
                xPos = player.X;
                yPos = player.Y;
            }

            transform.position = new Vector3(xPos, yPos, -10);
        }

        public void Init(Player player) {
            this.player = player;
            xPos = player.X;
            yPos = player.Y;

            transform.position = new Vector3(player.X, player.Y, -10);            
        }
    }
}