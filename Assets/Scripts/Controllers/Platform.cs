using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Controls the behaviour of platforms in the game.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(PlatformEffector2D), typeof(BoxCollider2D))]
    public class Platform : MonoBehaviour
    {
        [SerializeField] private bool oneWay = true;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        private void OnValidate()
        {
            GetComponent<BoxCollider2D>().usedByEffector = true;
            GetComponent<PlatformEffector2D>().useOneWay = oneWay;
        }
    }
}