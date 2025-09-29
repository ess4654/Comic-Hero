using ComicHero.Controllers;
using ComicHero.Data;
using TMPro;
using UnityEngine;

namespace ComicHero.Views.UI
{
    /// <summary>
    ///     XXX
    /// </summary>
    public class PlayerUI : PlayerComponent
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private Color[] playerColors;

        /// <summary>
        ///     Sets the player name above the player's head.
        /// </summary>
        /// <param name="name">Name of the player.</param>
        /// <param name="playerColor">The color of the player.</param>
        public void SetPlayerName(string name, PlayerColors playerColor)
        {
            if (playerName != null)
            {
                playerName.text = name;
                playerName.color = playerColors[(int)playerColor];
            }
        }
    }
}