using ComicHero.Controllers;
using UnityEngine;

namespace ComicHero.Views.UI
{
    /// <summary>
    ///     Manages the camera of the HUD.
    /// </summary>
    public class UICamera : Singelton<UICamera>
    {
        private int updatedPlayers;
        private const float threshold = 0.01f;

        private void OnEnable()
        {
            Player.PlayerCameraChanged += UpdateCameras;
        }

        private void OnDisable()
        {
            Player.PlayerCameraChanged -= UpdateCameras;
        }

        private void UpdateCameras()
        {
            updatedPlayers++;
            if (updatedPlayers == 2)
            {
                var leftPlayerIndex = PlayerManager.Instance.LeftPlayer.PlayerIndex;
                bool leftOn = leftPlayerIndex == 0 || PlayerManager.Instance.GetPlayerPosition(0) == PlayerManager.Instance.GetPlayerPosition(1);
                var first = transform.GetChild(0);
                var second = transform.GetChild(1);
                first.gameObject.SetActive(leftOn);
                second.gameObject.SetActive(!leftOn);

                RotateCameras(leftOn ? first : second, !leftOn);

                updatedPlayers = 0;
            }
        }

        private void RotateCameras(Transform activeCamera, bool flip)
        {
            var player1Position = PlayerManager.Instance.GetPlayerPosition(0);
            var player2Position = PlayerManager.Instance.GetPlayerPosition(1);
            var angle = Vector3.SignedAngle(player1Position, player2Position, Vector3.forward) * 100;
         
            if(Mathf.Abs(angle) > threshold)
                activeCamera.transform.localEulerAngles = new Vector3(0, 0, -angle * (flip ? -1 : 1));
        }
    }
}