using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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

        /// <summary>
        ///     Gets the player at the given index.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>Reference to the player.</returns>
        public Player GetPlayer(int playerIndex) => Players?[playerIndex];

        /// <summary>
        ///     Gets the position of the player at the given index.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>Global position of the player</returns>
        public Vector3 GetPlayerPosition(int playerIndex) =>
            GetPlayer(playerIndex).Position;

        /// <summary>
        ///     The leftmost player of the game.
        /// </summary>
        public Player LeftPlayer => Players.OrderBy(x => x.transform.position.x ).FirstOrDefault();

        /// <summary>
        ///     The rightmost player of the game.
        /// </summary>
        public Player RightPlayer => Players.OrderBy(x => x.transform.position.x).Reverse().FirstOrDefault();

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
                if(player.PlayerIndex % 2 == 1)
                    player.Flip();
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

        public void RespawnPlayer(int playerIndex)
        {
            GetPlayer(playerIndex).transform.position = spawnPoints[playerIndex].position;
        }

        public void DespawnPlayer(int playerIndex)
        {
            Destroy(Players[playerIndex].gameObject);
        }

        #endregion
    }
}