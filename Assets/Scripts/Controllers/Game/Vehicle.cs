using ComicHero.Views.Comic;
using UnityEngine;

namespace ComicHero.Controllers.Game
{
    /// <summary>
    ///     Controls the behavior of vehicles.
    /// </summary>
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Vehicle : ComicComponent
    {
        #region VARIABLE DECLARATIONS

        [SerializeField] private bool randomizeColor = true;
        [SerializeField] private float downTime = .15f;

        private bool down;

        #endregion

        #region ENGINE

        private void OnEnable()
        {
            if(randomizeColor)
            {
                if(!gameObject.TryGetComponent(out RandomComicHue randomHue))
                {
                    gameObject.AddComponent<RandomComicHue>();
                }
                else
                {
                    randomHue.enabled = false;
                    randomHue.enabled = true;
                }
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