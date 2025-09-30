using UnityEngine;

namespace ComicHero.Controllers.Game
{

    public class StickerSpawnManager : RandomScaler
    {
        [SerializeField] private SpriteRenderer[] stickers;
        
        private void Start()
        {
            if(Spawn)
            {
                Instantiate(stickers.SelectRandom(), transform);
            }
        }
    }
}