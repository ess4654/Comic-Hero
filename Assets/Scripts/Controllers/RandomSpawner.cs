using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Controls the random spawning of game objects.
    /// </summary>
    public abstract class RandomSpawner : RandomEvaluator
    {
        [SerializeField] private bool disableSpawn;
        [SerializeField, Min(0)] private float spawnProbability = 0.5f;

        protected bool Spawn => !disableSpawn && CheckProbability(spawnProbability);
        protected bool CheckSpawn(float probability) => !disableSpawn && CheckProbability(probability);
    }
}