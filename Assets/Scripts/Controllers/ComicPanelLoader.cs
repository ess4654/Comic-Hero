using ComicHero.Data;
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

        private enum BrakeSide
        {
            Left,
            Center,
            Right
        };

        private Transform leftBrake;
        private Transform rightBrake;
        private const float hopDistance = 17;

        private SpriteRenderer leftPanel;
        private SpriteRenderer rightPanel;
        
        private bool running;

        #endregion

        #region ENGINE

        private void OnEnable() => LoadComicPanels();

        private void Update()
        {
            if (!running) return;

            if(GameData.Mode == GameMode.EndlessRunner && !GameController.IsGameOver)
            {
                var leftPlayer = PlayerManager.Instance.LeftPlayer;
                var rightPlayer = PlayerManager.Instance.RightPlayer;
                var leftPlayerX = leftPlayer.Position.x;
                var rightPlayerX = rightPlayer.Position.x;

                if (leftPanel.transform.parent == null)
                {
                    if (leftPlayerX < leftBrake.position.x)
                        leftPanel = CreateRandomComicPanel(BrakeSide.Left);
                }
                if (rightPanel.transform.parent == null)
                {
                    if (rightPlayerX > rightBrake.position.x)
                        rightPanel = CreateRandomComicPanel(BrakeSide.Right);
                }
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        ///     Loads the comic panels into the game world.
        /// </summary>
        public void LoadComicPanels()
        {
            leftBrake = panel1Mount;
            rightBrake = panel3Mount;

            //remove old items
            if (GameData.Mode != GameMode.EndlessRunner)
                ItemSpawner.ClearSpawnedItems();

            //create the comic panels
            leftPanel = CreateRandomComicPanel(BrakeSide.Left);
            CreateRandomComicPanel(BrakeSide.Center);
            rightPanel = CreateRandomComicPanel(BrakeSide.Right);

            OnPanelsLoaded?.Invoke(); //subscribed event
            running = true;
        }

        private SpriteRenderer CreateRandomComicPanel(BrakeSide brake)
        {
            var gameMode = GameData.Mode;

            //get random comic panel
            var randomComic = comicPanels.SelectRandom();

            //randomly invert the brake
            var brakeTransform = GetBrake(brake);
            brakeTransform.localScale = new Vector3(Random.value < 0.5f ? -1 : 1, 1, 1);

            //remove old comic panels
            if (gameMode != GameMode.EndlessRunner)
            {
                if (brakeTransform.childCount > 0)
                    Destroy(brakeTransform.GetChild(0).gameObject);
            }
            else if(gameMode == GameMode.EndlessRunner)
            {
                if ((leftPanel != null && brake == BrakeSide.Left) || (rightPanel != null && brake == BrakeSide.Right))
                    HopBrake(brake);
            }

            //create new comic panel
            SpriteRenderer comic = Instantiate(randomComic, brakeTransform);

            //unmount the comic on endless runners
            if(gameMode == GameMode.EndlessRunner)
                comic.transform.SetParent(null, true);

            Debug.Log("Created Comic Panel On Brake: " + brake + $" {comic.transform.position}");
            return comic;
        }

        private void HopBrake(BrakeSide brake)
        {
            var hop = 0f;
            if (brake == BrakeSide.Left) hop = -hopDistance;
            else if (brake == BrakeSide.Right) hop = hopDistance;

            var brakeTransform = GetBrake(brake);
            brakeTransform.position = new Vector3(brakeTransform.position.x + hop, brakeTransform.position.y, brakeTransform.position.z);
        }

        private Transform GetBrake(BrakeSide brake)
        {
            if(brake == BrakeSide.Left)
                return leftBrake;
            else if (brake == BrakeSide.Center)
                return panel2Mount;
            else if (brake == BrakeSide.Right)
                return rightBrake;

            return null;
        }

        #endregion
    }
}