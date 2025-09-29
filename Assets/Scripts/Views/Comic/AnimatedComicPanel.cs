using UnityEngine;

namespace ComicHero.Views.Comic
{
    /// <summary>
    ///     Animates the comic panel
    /// </summary>
    public class AnimatedComicPanel : ComicComponent
    {
        #region VARIABLE DECLARATIONS

        [SerializeField, Min(0.1f)] private float animationSpeed = 1f;

        private ComicPanel.FillType fillType;
        
        private float animationProbability => 1f;
        private bool animating;
        private bool reverse;
        private bool biDirectional;
        private bool horitzontal;
        private bool rotateCheckered;
        private float randomRotationSpeed;

        #endregion

        protected override void Awake()
        {
            base.Awake();

            animating = Random.value <= animationProbability;
            reverse = Reverse;
            biDirectional = Reverse;
            horitzontal = Reverse;
            rotateCheckered = Reverse;
            randomRotationSpeed = Random.Range(.5f, 5f);
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
            if (animating)
            {
                if ((fillType == ComicPanel.FillType.Radial
                    || fillType == ComicPanel.FillType.Checkerboard
                    || fillType == ComicPanel.FillType.Stars
                    || fillType == ComicPanel.FillType.PokaDots
                    || fillType == ComicPanel.FillType.Stripes) && !IsLooping)
                    Tween();
            }
        }

        protected override float TweenTime => (1f / animationSpeed) * 10;

        protected override bool Reverse => Random.value < 0.5f;

        protected override void Tween(float lerp)
        {
            if(fillType == ComicPanel.FillType.Radial)
            {
                comic.Rotation = lerp * randomRotationSpeed;
            }
            else if(fillType == ComicPanel.FillType.Checkerboard
                || fillType == ComicPanel.FillType.Stars
                || fillType == ComicPanel.FillType.PokaDots)
            {
                if (rotateCheckered) //animate rotation
                {
                    comic.Rotation = lerp * randomRotationSpeed;
                }
                else //animate offset
                { 

                    if (biDirectional)
                        comic.Offset = new Vector2(lerp * (reverse ? -1 : 1), lerp * (reverse ? -1 : 1));
                    else if (horitzontal)
                        comic.Offset = new Vector2(lerp * (reverse ? -1 : 1), 0);
                    else
                        comic.Offset = new Vector2(0, lerp * (reverse ? -1 : 1));
                }
            }
            else if(fillType == ComicPanel.FillType.Stripes)
                comic.LineOffset = (reverse ? -1 : 1) * lerp;
        }
    }
}
