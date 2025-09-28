using ComicHero.Data;
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
        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private WeaponManager ui;
        [SerializeField] private PlayerData data;

        /// <summary>
        ///     The world position of the player.
        /// </summary>
        public Vector3 Position => transform.position;
        
        /// <summary>
        ///     The direction the player is facing.
        /// </summary>
        public int Facing { get; private set; }

        /// <summary>
        ///     The current health of the player.
        /// </summary>
        public float Health => data.health;

        /// <summary>
        ///     The number of lives the player has.
        /// </summary>
        public int Lives => data.lives;

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
                    fire = KeyCode.Tab
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
        private Rigidbody2D rigidbody;
        private PlayerPlatformInteractor platformInteractor;
        private bool jumping;

        #endregion

        private void Awake()
        {
            if (Facing == 0)
                Facing = 1;
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
            if (GameController.IsGameOver) return;

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
            if (input.Jumped(playerIndex) && !jumping)
            {
                jumping = true;
                Jump();
            }

            //fire projectile
            if(input.Fired(playerIndex))
            {
                LoadProjectile();
            }

            //flip spite
            if (inputAxis.x != 0 && inputAxis.x != Facing)
                Flip();

            //drop down from platform
            if (inputAxis.y < 0)
                platformInteractor.DisablePlatform();

            //call the animator to update
            if(jumping)
                animator.Jump();
            else if(inputAxis.x != 0)
                animator.Moving();
            else
                animator.SetIdle();
        }

        private void Jump()
        {
            rigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
        }

        private void LoadProjectile()
        {
            if(weaponManager != null)
                weaponManager.FireBullet();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            jumping = false;
            if (collision.gameObject.CompareTag("Hazard"))
                Die();
        }
        
        private bool isDead = false;

        private void Die()
        {
            if (isDead) return;
            isDead = true;

            //is the player defeated?
            if(data.lives == 0)
            {
                Defeated();
                return;
            }

            //respawn if we still have lives
            data.Die();
            data.ResetHealth();
            PlayerManager.Instance.RespawnPlayer(playerIndex);
            isDead = false;
        }

        private void Defeated() 
        {
            GameController.Instance.GameOver();
            PlayerManager.Instance.DespawnPlayer(playerIndex);
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
            if(Facing == 0)
                Facing = 1;

            Facing *= -1;
            if(sprite != null)
                sprite.flipX = !sprite.flipX;
        }

        #endregion
    }
}