using ComicHero.Controllers;
using UnityEngine;

namespace ComicHero
{

    public class GameController : Singelton<GameController>
    {
        private void Start()
        {
            PlayerManager.Instance.SpawnPlayers(2);
        }
    }
}