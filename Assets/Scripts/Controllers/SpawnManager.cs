using ComicHero.Controllers.Game;
using System.Collections.Generic;
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
        
        [Header("Scenery")]
        [SerializeField] private bool disableScenery;
        [SerializeField] private ScenerySpawner scenerySpawner;
        [SerializeField] private float scenerySpawnProbability = 0.5f;

        #endregion

        private void OnEnable()
        {
            if (Spawn)
            {
                var objectCache = new List<WorldObjects>(AvailableObjects);
                var itemToSpawn = AvailableObjects.SelectRandom();

                //Spawn vehicles
                bool spawnedVehicle = false;
                if (
                    itemToSpawn == WorldObjects.Vehicles &&
                    CheckProbability(vehicleSpawnProbability)
                    && vehicleSpawner != null &&
                    !disableVehicles)
                {
                    vehicleSpawner.SpawnVehicle(leftBrake, rightBrake);
                    spawnedVehicle = true;
                }

                if (!spawnedVehicle)
                {
                    //Spawn scenery
                    bool spawnedScenery = false;
                    objectCache.Remove(WorldObjects.Vehicles);
                    itemToSpawn = objectCache.SelectRandom();

                    if(
                        itemToSpawn == WorldObjects.Scenery &&
                        scenerySpawner != null &&
                        CheckProbability(scenerySpawnProbability) &&
                        !disableScenery)
                    {
                        scenerySpawner.SpawnScenery();
                        spawnedScenery = true;
                    }
                }
            }
        }
    }
}