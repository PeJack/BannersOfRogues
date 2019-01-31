using System;

namespace BannersOfRogues.Utils {
    public delegate void UpdateEventHandler(object sender, UpdateEventArgs e);

    public class UpdateEventArgs : EventArgs {
        /// <summary>
        /// Время в секундах с последнего обновления.
        /// </summary>
        public float Time { get; private set; }
        public UpdateEventArgs(float time) {
            this.Time = time;
        }
    }
}