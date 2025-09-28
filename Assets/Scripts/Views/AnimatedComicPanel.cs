using ComicHero.Views;
using UnityEngine;

namespace Assets.Scripts.Views
{
    /// <summary>
    ///     Animates the comic panel
    /// </summary>
    [RequireComponent(typeof(ComicPanel))]
    public class AnimatedComicPanel : LoopingLeanTween
    {
        [SerializeField, Min(0.1f)] private float animationSpeed = 1f;

        private ComicPanel comic;
        private ComicPanel.FillType fillType;

        private void Awake()
        {
            comic = GetComponent<ComicPanel>();
        }

        private void OnEnable()
        {
            comic.OnComicRendered += ComicRendered;
        }

        private void OnDisable()
        {
            comic.OnComicRendered += ComicRendered;
        }

        private void ComicRendered(ComicPanel.FillType fillType)
        {
            this.fillType = fillType;
            if (fillType == ComicPanel.FillType.Radial && !IsLooping)
                Tween();
        }

        protected override float TweenTime => (1f / animationSpeed) * 10;

        protected override bool Reverse => Random.value < 0.5f;

        protected override void Tween(float lerp)
        {
            if(fillType == ComicPanel.FillType.Radial)
                comic.Rotation = lerp * 360f;
        }
    }
}
