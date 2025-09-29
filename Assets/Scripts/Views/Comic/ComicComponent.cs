using UnityEngine;

namespace ComicHero.Views.Comic
{
    /// <summary>
    ///     Base class used by all comic components.
    /// </summary>
    [RequireComponent(typeof(ComicPanel))]
    public abstract class ComicComponent : LoopingLeanTween
    {
        protected ComicPanel comic { get; private set; }

        protected virtual void Awake()
        {
            comic = GetComponent<ComicPanel>();
        }
    }
}