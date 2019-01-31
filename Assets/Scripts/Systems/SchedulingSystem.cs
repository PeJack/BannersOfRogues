using System.Linq;
using System.Collections.Generic;
using BannersOfRogues.Interfaces;

namespace BannersOfRogues.Systems {
    public class SchedulingSystem {
        private int time;
        private readonly SortedDictionary<int, List<IScheduleable>> scheduleableList;
        private Game game;

        public SchedulingSystem(Game game) {
            time = 0;
            scheduleableList = new SortedDictionary<int, List<IScheduleable>>();
        }

        /// <summary>
        /// Добавить новый объект в планировщик. 
        /// </summary>
        public void Add(IScheduleable scheduleable) {
            int key = time + scheduleable.Time;

            if (!scheduleableList.ContainsKey(key)) {
                scheduleableList.Add(key, new List<IScheduleable>());
            }

            scheduleableList[key].Add(scheduleable);
        }

        /// <summary>
        /// Удалить объект из планировщика. Полезно в случае смерти врага, 
        /// чтобы предотвратить запланированную последовательность действий. 
        /// </summary>
        public void Remove(IScheduleable scheduleable) {
            KeyValuePair<int, List<IScheduleable>> schedule = new KeyValuePair<int, List<IScheduleable>>(-1, null);

            foreach (var _schedule in scheduleableList) {
                if (_schedule.Value.Contains(scheduleable)) {
                    schedule = _schedule;
                    break;
                }
            }

            if (schedule.Value != null) {
                schedule.Value.Remove(scheduleable);
                if (schedule.Value.Count <= 0) {
                    scheduleableList.Remove(schedule.Key);
                }
            }
        }

        public IScheduleable Get() {
            var firstList = scheduleableList.First();
            var firstScheduleable = firstList.Value.First();
            Remove(firstScheduleable);
            time = firstList.Key;
            return firstScheduleable;
        }
        
        public int GetTime() {
            return time;
        }

        public void Clear() {
            time = 0;
            scheduleableList.Clear();
        }
    }
}