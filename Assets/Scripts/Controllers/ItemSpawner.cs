using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Manages spawning of items.
    /// </summary>
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField, Min(0)] private float spawnProbability = 0.5f;
        [SerializeField] private GameObject[] items;

        private static Transform spawnBin;

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
            if(Random.value > Mathf.Clamp01(spawnProbability))
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
    }
}