using ComicHero.Data;
using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Controls the behaviour of collectibles.
    /// </summary>
    public class Collectible : MonoBehaviour
    {
        public CollectibleType Type => type;
        [SerializeField] private CollectibleType type;
        [SerializeField] private SpriteRenderer cloud;
        [SerializeField] private Sprite cloudSprite;

        private void Awake()
        {
            if(cloud != null && Random.value < 0.5f)
                cloud.sprite = cloudSprite;

            Tween();
        }

        private void Tween()
        {
            //Tween();
        }
    }
}