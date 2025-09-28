using System.Collections;
using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Used to control the passing through platforms when jumping down.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerPlatformInteractor : MonoBehaviour
    {
        [SerializeField] private string platformTag = "Platform";
        [SerializeField, Min(0.25f)] private float fallTimer = 3f;

        private BoxCollider2D playerCollider;
        private BoxCollider2D currentPlatform;

        #region ENGINE

        private void Awake()
        {
            playerCollider = GetComponent<BoxCollider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag(platformTag))
                currentPlatform = collision.gameObject.GetComponent<BoxCollider2D>();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(platformTag))
                currentPlatform = null;
        }

        private IEnumerator DisableCollision()
        {
            if(currentPlatform != null)
            {
                var _currentPlatform = currentPlatform;
                Physics2D.IgnoreCollision(playerCollider, _currentPlatform);
                yield return new WaitForSeconds(fallTimer);
                if (_currentPlatform != null)
                {
                    Physics2D.IgnoreCollision(playerCollider, _currentPlatform, false);
                    currentPlatform = null;
                }
            }
        }

        #endregion

        /// <summary>
        ///     Disables the platform we are currently standing on.
        /// </summary>
        public void DisablePlatform() => StartCoroutine(DisableCollision());
    }
}