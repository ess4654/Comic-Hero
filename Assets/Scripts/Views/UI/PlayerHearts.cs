using ComicHero.Controllers;
using ComicHero.Data;
using System.Collections.Generic;
using UnityEngine;

namespace ComicHero.Views.UI
{
    /// <summary>
    ///     Controls the displaying of the hearts.
    /// </summary>
    public class PlayerHearts : PingPongLeanTween
    {
        [SerializeField] private bool isLeft = false;

        private List<GameObject> hearts;

        private bool flip;
        private const float minScale = 0.5f;
        private const float maxScale = 1.5f;

        #region ENGINE

        private void OnEnable()
        {
            hearts = new List<GameObject>();
            for(int i = 0; i < transform.childCount; i++)
                hearts.Add(transform.GetChild(i).gameObject);

            Tween();
        }

        protected override float TweenTime => 0.5f;

        protected override void PingPong(float lerp)
        {
            var scale = Mathf.Lerp(minScale, maxScale, lerp);
            foreach(var heart in hearts)
                heart.transform.localScale = scale * Vector3.one;
        }

        private void Update()
        {
            if(GameController.IsGameOver) return;

            var player = isLeft ? PlayerManager.Instance.LeftPlayer : PlayerManager.Instance.RightPlayer;
            var lives = player.Lives;
            for(var i = 0; i < Constants.MaxLives; i++)
            {
                bool on = i < lives;
                transform.GetChild(i).gameObject.SetActive(on);
            }
        }

        #endregion
    }
}