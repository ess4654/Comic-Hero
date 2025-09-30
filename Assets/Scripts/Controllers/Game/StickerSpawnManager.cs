using UnityEngine;

namespace ComicHero.Controllers.Game
{
    public class StickerSpawnManager : RandomScaler
    {
        [SerializeField] private SpriteRenderer[] stickers;
        
        private void Start()
        {
            if(Spawn && stickers.Length > 0 && transform.childCount > 0)
            {
                Instantiate(stickers.SelectRandom(), transform.GetChild(Range(0, transform.childCount)));
            }
        }
    }
}