using System;
using UnityEngine;

namespace ComicHero.Data
{
    [Serializable]
    public class PlayerData
    {
        public const float MaxHealth = 100;
        public const int MaxLives = 3;

        public float health = MaxHealth;
        public int lives = MaxLives;

        public void TakeDamage(float damage, Action onDead)
        {
            health -= Mathf.Abs(damage);
            if(health < 0 )
            {
                onDead?.Invoke();
                lives--;
            }
        }

        public void Die() => lives--;

        public void ResetHealth()
        {
            health = MaxHealth;
        }

        public void Reset()
        {
            health = MaxHealth;
            lives = MaxLives;
        }
    }
}