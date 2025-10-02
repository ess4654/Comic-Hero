using UnityEngine;

namespace ComicHero.Views.Game
{
    public class GrowingBust : PingPongLeanTween
    {
        [SerializeField] private SpriteRenderer normalTexture;
        [SerializeField] private SpriteRenderer buffTexture;
        [SerializeField] private float growTime = .5f;
        [SerializeField] private Vector3 growScale = 1.5f * Vector3.one;

        private bool isGrown;

        public override bool IsLooping
        {
            get => false;
            protected set => base.IsLooping = false;
        }

        protected override float TweenTime => growTime;

        protected override void PingPong(float lerp)
        {
            var scale = Vector3.Lerp(Vector3.one, growScale, lerp);
            transform.localScale = scale;

            if(lerp > 0.5f)
            {
                normalTexture.gameObject.SetActive(false);
                buffTexture.gameObject.SetActive(true);
            }
            else
            {
                normalTexture.gameObject.SetActive(true);
                buffTexture.gameObject.SetActive(false);
            }
        }

        [ContextMenu("Grow")]
        public void Grow()
        {
            if (!isGrown)
            {
                isGrown = true;
                Tween();
            }
        }

        [ContextMenu("Shrink")]
        public void Shrink()
        {
            if (isGrown)
            {
                isGrown = false;
                Tween();
            }
        }
    }
}