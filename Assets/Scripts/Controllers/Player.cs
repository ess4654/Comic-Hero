using System;
using UnityEngine;
using UnityEngine.Rendering;

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

        /// <summary>
        ///     The world posiiton of the player.
        /// </summary>
        public Vector3 Position => transform.position;

        protected Player OtherPlayer => PlayerManager.Instance.Players[(PlayerIndex + 1) % 2];

        #endregion

        #region ENGINE

        public static event Action PlayerCameraChanged;

        private void OnEnable()
        {
            RenderPipelineManager.beginCameraRendering += UpdatePlayerCamera;
        }

        private void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering += UpdatePlayerCamera;
        }

        private void UpdatePlayerCamera(ScriptableRenderContext context, Camera camera)
        {
            if (playerCamera != null)
            {
                var otherPlayerX = OtherPlayer.transform.position.x;
                var playerX = transform.position.x;
                var samePosition = playerX == otherPlayerX;
                bool isLeft = otherPlayerX >= playerX;
                var cameraX = (isLeft ? -1 : 1) * cameraOffset;
                playerCamera.transform.localPosition = new Vector3(cameraX * (samePosition && playerIndex == 1 ? -1 : 1), 0, -10);

                PlayerCameraChanged?.Invoke(); //subscribed event
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