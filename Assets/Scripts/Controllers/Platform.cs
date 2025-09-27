using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Controls the behaviour of platforms in the game.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Platform : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}