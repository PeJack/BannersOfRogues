using System;
using System.Collections.Generic;
using RogueSharp.Random;

namespace BannersOfRogues.Systems {
    public class GamePool<T> {
        private readonly List<PoolEntity<T>> pooledEntites;
        private static readonly IRandom random = new DotNetRandom();
        private int totalWeight;

        public GamePool() {
            pooledEntites = new List<PoolEntity<T>>();
        }

        public T Get() {
            int runningWeight = 0;
            int roll = random.Next(1, totalWeight);

            foreach (var pooledEntity in pooledEntites) {
                runningWeight += pooledEntity.Weight;
                if (roll <= runningWeight) {
                    Remove(pooledEntity);
                    return pooledEntity.Entity;
                }
            }

            throw new InvalidOperationException("Не получено ни одной сущности из пула.");
        }

        public void Add(T entity, int weight) {
            pooledEntites.Add(new PoolEntity<T> { Entity = entity, Weight = weight});
            totalWeight += weight;
        }

        public void Remove(PoolEntity<T> entity) {
            pooledEntites.Remove(entity);
            totalWeight -= entity.Weight;
        }
    }

    public class PoolEntity<T> {
        public int  Weight  { get; set; }
        public T    Entity    { get; set; }
    }
}