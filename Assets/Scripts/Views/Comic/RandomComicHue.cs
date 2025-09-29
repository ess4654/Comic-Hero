using UnityEngine;

namespace ComicHero.Views.Comic
{
    /// <summary>
    ///     Randomizes the hue of comic component when loaded.
    /// </summary>
    public class RandomComicHue : ComicComponent
    {
        protected override float TweenTime => 1;

        protected override void Tween(float lerp) { }

        private void OnEnable()
        {
            comic.Hue = Random.Range(-180, 180);
        }
    }
}