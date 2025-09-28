using ComicHero.Controllers;
using ComicHero.Data;
using UnityEngine;

namespace ComicHero.Views.UI
{
    /// <summary>
    ///     Controls the displaying of the hearts.
    /// </summary>
    public class PlayerHearts : MonoBehaviour
    {
        [SerializeField] private bool isLeft = false;

        private void Update()
        {
            if(GameController.IsGameOver) return;

            var player = isLeft ? PlayerManager.Instance.LeftPlayer : PlayerManager.Instance.RightPlayer;
            var lives = player.Lives;
            for(var i = 0; i < PlayerData.MaxLives; i++)
            {
                bool on = i < lives;
                transform.GetChild(i).gameObject.SetActive(on);
            }
        }
    }
}