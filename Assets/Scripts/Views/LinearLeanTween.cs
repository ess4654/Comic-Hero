namespace ComicHero.Views
{
    /// <summary>
    ///     Used for animations that do not repeat.
    /// </summary>
    public abstract class LinearLeanTween : LoopingLeanTween
    {
        public override bool IsLooping
        { 
            get => false;
            protected set => IsLooping = false;
        }
    }
}