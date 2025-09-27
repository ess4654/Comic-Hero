using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Manages spawning of items.
    /// </summary>
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] items;

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
            if(UnityEngine.Random.value < 0.5)
            {
                Destroy(gameObject);
                return;
            }

            var itemToSpawn = items.SelectRandom();
            if(itemToSpawn != null)
                Instantiate(itemToSpawn, transform);
        }
    }
}