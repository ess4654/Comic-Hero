using System.Collections.Generic;
using UnityEngine;

namespace ComicHero.Controllers.Game
{
    public class StickerSpawner : RandomScaler
    {
        [SerializeField] private SpriteRenderer[] stickers;
        [SerializeField, Min(0)] private int maxStickers = 4;
        [SerializeField] private float maxRotation = 30f;

        private List<Transform> availableSpawnPoints;

        private void OnEnable()
        {
            if(Spawn && stickers.Length > 0 && transform.childCount > 0 && maxStickers != 0)
            {
                //get all available spawn points
                availableSpawnPoints = new List<Transform>();
                for(var i = 0; i < transform.childCount; i++) 
                    availableSpawnPoints.Add(transform.GetChild(i).transform);

                //spawn the number of stickers we want
                var stickersToSpawn = Range(1, maxStickers);
                for (int i = 0; i < stickersToSpawn; i++)
                {
                    var spawnPoint = availableSpawnPoints.SelectRandom();
                    var sticker = Instantiate(stickers.SelectRandom(), spawnPoint);
                    if (Scale)
                    {
                        var flip = FiftyFifty;
                        float scale = Range(.75f, 2f);
                        sticker.transform.localScale = new Vector3((scale * sticker.transform.localScale.x) * (flip ? -1f : 1f), scale * sticker.transform.localScale.y, scale * sticker.transform.localScale.z);
                    }

                    sticker.transform.localEulerAngles = new Vector3(0, 0, Range(-maxRotation, maxRotation));
                }
            }
        }
    }
}