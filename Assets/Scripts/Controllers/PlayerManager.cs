using ComicHero;
using System.Collections.Generic;
using UnityEngine;

namespace ComicHero.Controllers
{
    /// <summary>
    ///     Manages spawning of <see cref="Player"/>.
    /// </summary>
    public class PlayerManager : Singelton<PlayerManager>
    {
        #region VARIABLE DECLARATIONS

        [SerializeField] private Player playerPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private RenderTexture[] playerCameraTextures;

        /// <summary>
        ///     All spawned players in the level.
        /// </summary>
        public List<Player> Players { get; private set; }

        #endregion

        #region METHODS

        /// <summary>
        ///     Spawns the number of players into the game world.
        /// </summary>
        /// <param name="count">Number of players to spawn.</param>
        public void SpawnPlayers(int count)
        {
            Players = new List<Player>();
            for(int i = 0; i < Mathf.Max(count, spawnPoints.Length); i++)
            {
                var player = SpawnPlayer(spawnPoints[i].position);
                player.PlayerIndex = i;
                if(i < playerCameraTextures.Length)
                    player.SetCameraTexture(playerCameraTextures[i]);
                Players.Add(player);
            }
        }

        private Player SpawnPlayer(Vector3 position)
        {
            if (playerPrefab != null)
            {
                var player = Instantiate(playerPrefab);
                player.transform.position = position;
                return player;
            }
            else
                return null;
        }

        /// <summary>
        ///     Despawns all players from the world.
        /// </summary>
        public void DespawnPlayers()
        {
            if (Players != null)
            {
                foreach (var player in Players)
                    Destroy(player.gameObject);
                Players.Clear();
            }
        }

        #endregion
    }
}