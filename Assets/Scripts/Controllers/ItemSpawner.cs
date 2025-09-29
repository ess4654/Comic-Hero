using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Manages spawning of items.
    /// </summary>
    public class ItemSpawner : RandomSpawner
    {
        [SerializeField] private GameObject[] items;

        private static Transform spawnBin;

        #region ENGINE

        private void OnEnable()
        {
            ComicPanelLoader.OnPanelsLoaded += PanelsLoaded;
        }

        private void OnDisable()
        {
            ComicPanelLoader.OnPanelsLoaded -= PanelsLoaded;
        }

        private void PanelsLoaded()
        {
            if(!Spawn)
            {
                Destroy(gameObject);
                return;
            }

            if (spawnBin == null)
                spawnBin = new GameObject("@Items").transform;

            var itemToSpawn = items.SelectRandom();
            if(itemToSpawn != null)
            {
                var item = Instantiate(itemToSpawn, spawnBin);
                item.transform.position = transform.position;
            }
        }

        #endregion

        public static void ClearSpawnedItems()
        {
            if(spawnBin != null)
            {
                for(var i = 0; i < spawnBin.childCount; i++)
                    Destroy(spawnBin.GetChild(i));
            }
        }
    }
}