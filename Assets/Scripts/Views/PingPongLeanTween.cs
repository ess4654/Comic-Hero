namespace ComicHero.Views
{
    /// <summary>
    ///     Base class used by all animations that ping pong.
    /// </summary>
    public abstract class PingPongLeanTween : LoopingLeanTween
    {
        private bool flip;

        #region ENGINE

        protected override void Tween(float lerp)
        {
            lerp = flip ? (1 - lerp) : lerp;
            PingPong(lerp); //engine
        }

        protected override void OnComplete()
        {
            flip = !flip;
        }

        protected abstract void PingPong(float lerp);

        #endregion
    }
}