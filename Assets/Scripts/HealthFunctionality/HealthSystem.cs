using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthSystem
{
    public delegate void HealthPointsChangedEvent(int before, int after);
    [Serializable]
    public class Health
    {
        /// <summary>
        /// Max value and base value for <see cref="HP"/>
        /// <para>HP = health points</para>
        /// </summary>
        public int MaxHP { get; set; }

        /// <summary>
        /// Event risen when <see cref="HP"/> reach >=0
        /// </summary>
        public event Action OnDeath = delegate { };

        /// <summary>
        /// Current health points value
        /// </summary>
        public int HP
        {
            get => _hp;
            set => _hp = Clamp(value, 0, MaxHP);
        }

        private int _hp;

        public Health(int maxHp)
        {
            MaxHP = maxHp;
            HP = maxHp;
        }

        /// <summary>
        /// Reduces <see cref="HP"/> for the given value
        /// <para>Raises <see cref="OnDeath"/> (if <see cref="HP"/> is less or equal to 0)</para>
        /// </summary>
        /// <param name="damagePoints">The amount of points to lose</param>
        public void TakeDamage(int damagePoints)
        {
            if (damagePoints < 0)
                Console.WriteLine($"{nameof(damagePoints)} is < 0. This is not intended and might yield unwanted results" +
                                  $"\n it's recommended to use {nameof(Heal)}(healPoints) instead");
            HP -= damagePoints;
            if (HP <= 0)
                OnDeath.Invoke();
        }

        public void Kill() => TakeDamage(HP);

        /// <summary>
        /// Increases <see cref="HP"/> for the given value
        /// </summary>
        /// <param name="healPoints">The amount of points to heal</param>
        public void Heal(int healPoints)
        {
            if (healPoints < 0)
                Console.WriteLine($"{nameof(healPoints)} is < 0. This is not intended and might yield unwanted results" +
                                  $"\n it's recommended to use {nameof(TakeDamage)}(damagePoints) instead");
            HP += healPoints;
        }

        /// <summary>
        /// Fully heals this instance
        /// </summary>
        public void FullyHeal() => Heal(MaxHP);

        private static int Clamp(int value, int min, int max)
        {
            if (min > max)
                min = max;
            if (value > max)
                value = max;
            else if (value < min)
                value = min;
            return value;
        }
    }
}
