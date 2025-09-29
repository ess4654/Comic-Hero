using ComicHero.Views;
using ComicHero.Views.Comic;
using UnityEngine;

namespace ComicHero.Controllers.Game
{
    /// <summary>
    ///     Controls the behavior of vehicles.
    /// </summary>
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Vehicle : LinearLeanTween
    {
        #region VARIABLE DECLARATIONS

        [SerializeField] private bool randomizeColor = true;
        [SerializeField] private bool randomizeDirection = true;
        [SerializeField] private float downTime = .15f;
        [SerializeField] private SpriteRenderer sprite;

        private bool down;

        #endregion

        #region ENGINE

        private void OnEnable()
        {
            if (sprite != null)
            {
                if (randomizeColor)
                {
                    if (!sprite.gameObject.TryGetComponent(out RandomComicHue randomHue))
                    {
                        sprite.gameObject.AddComponent<RandomComicHue>();
                    }
                    else
                    {
                        randomHue.enabled = false;
                        randomHue.enabled = true;
                    }
                }

                if (randomizeDirection)
                    sprite.flipX = Random.value < 0.5f;
            }
        }

        protected override float TweenTime => downTime;
        
        protected override bool Reverse => down;
        
        protected override void Tween(float lerp)
        {
            down = !down;
        }

        #endregion
    }
}