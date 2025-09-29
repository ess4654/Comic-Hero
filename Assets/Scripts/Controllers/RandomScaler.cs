using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Base call used by all spawners that scale their spawned object.
    /// </summary>
    public abstract class RandomScaler : RandomSpawner
    {
        [SerializeField, Min(0)] private float scaleProbability = 0.5f;

        protected bool Scale => CheckProbability(scaleProbability);
        protected bool CheckScale(float probability) => CheckProbability(probability);
    }
}