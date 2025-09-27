using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Controls the movement of the player in the world.
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region VARIABLE DECLARATIONS

        /// <summary>
        ///     The index of the player when spawned.
        /// </summary>
        public int PlayerIndex
        {
            get => playerIndex;
            set => playerIndex = value;
        }
        [SerializeField] private int playerIndex;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float cameraOffset;

        protected Player OtherPlayer => PlayerManager.Instance.Players[(PlayerIndex + 1) % 2];

        #endregion

        #region ENGINE

        public void FixedUpdate()
        {
            if (playerCamera != null)
            {
                var otherPlayerX = OtherPlayer.transform.position.x;
                bool isLeft = otherPlayerX > transform.position.x;
                var cameraX = (isLeft ? -1 : 1) * cameraOffset;
                playerCamera.transform.localPosition = new Vector3(cameraX, 0, -10);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        ///     Sets the player's camera texture to render to.
        /// </summary>
        /// <param name="cameraTexture">The texture to render the camera to.</param>
        public void SetCameraTexture(RenderTexture cameraTexture)
        {
            if (playerCamera != null)
                playerCamera.targetTexture = cameraTexture;
        }

        #endregion
    }
}