using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Controls the weapons.
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private Transform weaponSlot;
        [SerializeField] private SpriteRenderer weapon;
        [SerializeField] private Rigidbody2D[] projectiles;
        [SerializeField] private float projectileLaunchVelocity = 1f;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private AudioSource soundFX;
        [SerializeField] private AudioClip[] weaponSounds;

        private Player player;
        private float xOffset;

        private void Awake()
        {
            player = GetComponent<Player>();
            if(projectileSpawnPoint != null)
                xOffset = projectileSpawnPoint.localPosition.x;
        }

        private void Update()
        {
            if(!GameController.IsGameOver)
            {
                if (projectileSpawnPoint != null)
                    projectileSpawnPoint.localPosition = new Vector3(xOffset * player.Facing, projectileSpawnPoint.localPosition.y, projectileSpawnPoint.localPosition.z);
            
                if(weapon != null)
                    weapon.flipX = player.Facing == -1;
            }
        }


        public void FireBullet()
        {
            var projectileBody = Instantiate(projectiles.SelectRandom(), projectileSpawnPoint);
            projectileBody.transform.localEulerAngles = new Vector3(0, 0, projectileBody.transform.localEulerAngles.z * player.Facing);
            projectileBody.transform.SetParent(null, true);
            var scale = projectileBody.transform.localScale.x;
            projectileBody.transform.localScale = new Vector3(player.Facing * scale, scale, scale);

            projectileBody.AddForce(new Vector2(projectileLaunchVelocity * player.Facing, 0));

            if (soundFX != null)
            {
                var weaponSound = weaponSounds.SelectRandom();
                soundFX.PlayOneShot(weaponSound);
            }
        }
    }
}