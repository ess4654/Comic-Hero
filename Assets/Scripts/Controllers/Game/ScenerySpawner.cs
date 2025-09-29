using UnityEngine;

namespace ComicHero.Controllers.Game
{
    /// <summary>
    ///     Controls the spawning of scenery in the game world.
    /// </summary>
    public class ScenerySpawner : RandomScaler
    {
        #region VARIABLE DECLARATIONS

        private enum SceneryObjects
        {
            GrafittiWall
        }
    
        private readonly SceneryObjects[] AvailableObjects = new SceneryObjects[]
        {
            SceneryObjects.GrafittiWall
        };

        [SerializeField] private GameObject[] grafittiWalls;

        #endregion

        public void SpawnScenery()
        {
            var objectToSpawn = AvailableObjects.SelectRandom();

            if(objectToSpawn == SceneryObjects.GrafittiWall)
            {
                var wall = Instantiate(grafittiWalls.SelectRandom());
                wall.transform.position = transform.position;
                wall.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            }
        }
    }
}