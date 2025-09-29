using ComicHero.Data;
using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Base class used by components of the player.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public abstract class PlayerComponent : MonoBehaviour
    {
        protected Player player { get; private set; }

        /// <summary>
        ///     The color of the player.
        /// </summary>
        public PlayerColors Color => player.Color;

        /// <summary>
        ///     The direction the player is facing.
        /// </summary>
        public int Facing => player.Facing;

        /// <summary>
        ///     The current health of the player.
        /// </summary>
        public float Health => player.Health;

        /// <summary>
        ///     The number of lives the player has.
        /// </summary>
        public int Lives => player.Lives;

        protected virtual void Awake()
        {
            player = GetComponent<Player>();
            if(player == null)
                player = GetComponentInParent<Player>();
        }
    }
}