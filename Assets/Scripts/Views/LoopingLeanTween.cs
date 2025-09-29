using UnityEngine;

namespace ComicHero.Views
{
    /// <summary>
    ///     Base class used by animations that loop.
    /// </summary>
    public abstract class LoopingLeanTween : MonoBehaviour
    {
        public virtual bool IsLooping { get; protected set; }

        #region ENGINE

        protected void Tween()
        {
            bool reverse = !IsLooping && Reverse;
            IsLooping = true;
            LeanTween
                .value(reverse ? 1f : 0f, reverse ? 0f : 1f, Mathf.Max(TweenTime, 0))
                .setOnUpdate(Tween)
                .setOnComplete(() =>
                {
                    OnComplete(); //engine

                    if (this == null || !IsLooping) return;
                    Tween();
                })
                .setEaseLinear();
        }

        protected virtual bool Reverse => false;

        protected abstract float TweenTime { get; }

        protected abstract void Tween(float lerp);
        
        protected virtual void OnComplete() { }

        #endregion

        #region METHODS

        public void StopTween() => IsLooping = false;

        #endregion
    }
}