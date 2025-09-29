using ComicHero.Controllers.Game;
using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Manages the spawning of 
    /// </summary>
    public class SpawnManager : RandomSpawner
    {
        #region VARIABLE DECLARATIONS

        private enum WorldObjects
        {
            Vehicles,
            Scenery
        }

        private readonly WorldObjects[] AvailableObjects = new WorldObjects[]
        {
            WorldObjects.Vehicles,
            WorldObjects.Scenery
        };

        [SerializeField] private Transform leftBrake;
        [SerializeField] private Transform rightBrake;

        [Header("Vehicles")]
        [SerializeField] private bool disableVehicles;
        [SerializeField] private float vehicleSpawnProbability = 0.1f;
        [SerializeField] private VehicleSpawner vehicleSpawner;

        #endregion

        private void OnEnable()
        {
            if (Spawn)
            {
                bool spawnedVehicle = false;
                var itemToSpawn = AvailableObjects.SelectRandom();

                //Spawn vehicles
                if (
                    itemToSpawn == WorldObjects.Vehicles &&
                    CheckProbability(vehicleSpawnProbability)
                    && vehicleSpawner != null &&
                    !disableVehicles)
                {
                  vehicleSpawner.SpawnVehicle(leftBrake, rightBrake);
                  spawnedVehicle = true;
                }
            }
        }
    }
}