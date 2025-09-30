using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Base class used by all scripts that evaluate a probability.
    /// </summary>
    public abstract class RandomEvaluator : MonoBehaviour
    {
        protected float RandomValue => Random.value;

        protected bool CheckProbability(float probability) => RandomValue <= Mathf.Clamp01(probability);
        
        protected bool FiftyFifty => CheckProbability(0.5f);

        protected float Range(float min, float max) => Random.Range(min, max);

        protected int Range(int min, int max) => Random.Range(min, max);
    }
}