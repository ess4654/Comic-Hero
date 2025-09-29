using ComicHero.Controllers;
using ComicHero.Views.Comic;
using UnityEngine;

namespace ComicHero.Views.UI
{
    /// <summary>
    ///     Updates the healthbar for the player.
    /// </summary>
    public class HealthBarUI : ComicComponent
    {
        [SerializeField] private Color healthColor = Color.red;

        private SpriteRenderer rend;
        private MaterialPropertyBlock props;
        private Player player;
        private const float rotation = 89.5f;
        private const float offset = .9f;

        protected override void Awake()
        {
            base.Awake();

            player = transform.GetComponentInParent<Player>();
            rend = GetComponent<SpriteRenderer>();
            props = new MaterialPropertyBlock();
            rend.GetPropertyBlock(props);

            comic.Fill = ComicPanel.FillType.Gradient;
            comic.Gradient = new ComicPanel.GradientSetting
            {
                color1 = healthColor,
                color2 = Color.black,
                rotation = rotation
            };
        }

        #region ENGINE

        protected override float TweenTime => 1;

        protected override void Tween(float lerp) { }

        private void Update()
        {
            comic.GradientOffset = player.Health == 100 ? -2 : (-0.01f * player.Health + offset);
        }

        #endregion
    }
}