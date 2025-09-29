using System;
using UnityEngine;
using static ComicHero.Data.Constants;

namespace ComicHero.Data
{
    /// <summary>
    ///     Container of data for the player.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        #region VARIABLE DECLARATIONS

        public float health = MaxHealth;
        public int lives = MaxLives;
        public PlayerColors color = AvailablePlayerColors.SelectRandom();

        #endregion

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

        public void ResetHealth() => health = MaxHealth;

        public void Reset()
        {
            health = MaxHealth;
            lives = MaxLives;
        }
    }
}