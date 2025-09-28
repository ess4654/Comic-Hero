using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Used to randomly load different comic panels into the game world.
    /// </summary>
    public class ComicPanelLoader : Singelton<ComicPanelLoader>
    {
        #region VARIABLE DECLARATIONS

        /// <summary>
        ///     Called when all comic panels have been loaded.
        /// </summary>
        public static event System.Action OnPanelsLoaded;

        [SerializeField] private SpriteRenderer[] comicPanels;
        [SerializeField] private Transform panel1Mount;
        [SerializeField] private Transform panel2Mount;
        [SerializeField] private Transform panel3Mount;

        #endregion

        private void OnEnable() => LoadComicPanels();

        #region METHODS

        /// <summary>
        ///     Loads the comic panels into the game world.
        /// </summary>
        public void LoadComicPanels()
        {
            //remove old comic panels
            if(panel1Mount.childCount > 0)
                Destroy(panel1Mount.GetChild(0).gameObject);
            if (panel2Mount.childCount > 0)
                Destroy(panel2Mount.GetChild(0).gameObject);
            if (panel3Mount.childCount > 0)
                Destroy(panel3Mount.GetChild(0).gameObject);

            //remove old items
            ItemSpawner.ClearSpawnedItems();

            //get random comic panels
            var panelA = comicPanels.SelectRandom();
            var panelB = comicPanels.SelectRandom();
            var panelC = comicPanels.SelectRandom();

            //create the comic panels
            Instantiate(panelA, panel1Mount);
            Instantiate(panelB, panel2Mount);
            Instantiate(panelC, panel3Mount);

            //randomly invert the comic panel
            panel1Mount.localScale = new Vector3(Random.value < 0.5f  ? - 1 : 1, 1, 1);
            panel2Mount.localScale = new Vector3(Random.value < 0.5f  ? - 1 : 1, 1, 1);
            panel3Mount.localScale = new Vector3(Random.value < 0.5f  ? - 1 : 1, 1, 1);

            OnPanelsLoaded?.Invoke(); //subscribed event
        }

        #endregion
    }
}