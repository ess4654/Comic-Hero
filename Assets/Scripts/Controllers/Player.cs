using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Controls the movement of the player in the world.
    /// </summary>
    [RequireComponent (typeof(Rigidbody2D), typeof(PlayerPlatformInteractor))]
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
        [SerializeField, Min(0.1f)] private float movementSpeed = 1f;
        [SerializeField, Min(0.1f)] private float jumpForce = 3f;
        [SerializeField] private SpriteRenderer sprite;

        /// <summary>
        ///     The world position of the player.
        /// </summary>
        public Vector3 Position => transform.position;

        protected Player OtherPlayer => PlayerManager.Instance.Players[(PlayerIndex + 1) % 2];

        private readonly ControllerScheme input = new ControllerScheme
        {
            Controls = new ControllerScheme.InputKeys[]
            {
                //Player 1 controls
                new ControllerScheme.InputKeys
                {
                    moveLeft = KeyCode.A,
                    moveRight = KeyCode.D,
                    jumpDown = KeyCode.S,
                    jump = KeyCode.LeftShift,
                    fire = KeyCode.RightShift
                },

                //Player 2 controls
                new ControllerScheme.InputKeys
                {
                    moveLeft = KeyCode.LeftArrow,
                    moveRight = KeyCode.RightArrow,
                    jumpDown = KeyCode.DownArrow,
                    jump = KeyCode.RightShift,
                    fire = KeyCode.Return
                }
            }
        };
        private Vector2 inputAxis;
        private int facing;
        private Rigidbody2D rigidbody;
        private PlayerPlatformInteractor platformInteractor;

        #endregion

        private void Awake()
        {
            if (facing == 0)
                facing = 1;
            rigidbody = GetComponent<Rigidbody2D>();
            platformInteractor = GetComponent<PlayerPlatformInteractor>();
        }

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

        private void FixedUpdate()
        {
            rigidbody.AddForce(movementSpeed * Time.smoothDeltaTime * inputAxis, ForceMode2D.Impulse);
        }

        private void LateUpdate()
        {
            //get the input values
            inputAxis = Vector2.zero;
            if (input.Left(playerIndex))
                inputAxis.x--;
            if (input.Right(playerIndex))
                inputAxis.x++;
            if (input.Down(playerIndex))
                inputAxis.y--;

            //jump
            if (input.Jumped(playerIndex))
                Jump();

            //flip spite
            if (inputAxis.x != 0 && inputAxis.x != facing)
                Flip();

            //drop down from platform
            if (inputAxis.y < 0)
                platformInteractor.DisablePlatform();
        }

        private void Jump()
        {
            rigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
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

        /// <summary>
        ///     Flips the direction the sprite is being rendered.
        /// </summary>
        public void Flip()
        {
            if(facing == 0)
                facing = 1;

            facing *= -1;
            if(sprite != null)
                sprite.flipX = !sprite.flipX;
        }

        #endregion
    }
}