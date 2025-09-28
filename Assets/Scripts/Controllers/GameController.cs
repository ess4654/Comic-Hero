using ComicHero.Controllers;

namespace ComicHero
{
    public class GameController : Singelton<GameController>
    {
        public static bool IsGameOver { get; private set; }

        private void Start()
        {
            PlayerManager.Instance.SpawnPlayers(2);
        }

        public void GameOver()
        {
            IsGameOver = true;
        }
    }
}