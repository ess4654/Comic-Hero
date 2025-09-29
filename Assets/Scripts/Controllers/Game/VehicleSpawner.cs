using UnityEngine;

namespace ComicHero.Controllers.Game
{
    /// <summary>
    ///     Controls the random spawn chance of vehicles;
    /// </summary>
    public class VehicleSpawner : RandomScaler
    {
        [SerializeField] private Vehicle[] vehicles;

        private const float minVehicleScale = .4f;

        public void SpawnVehicle(Transform leftBrake, Transform rightBrake)
        {
            var vehicle = Instantiate(vehicles.SelectRandom());
            var center = transform.position.x;

            //set position and rotation
            vehicle.transform.position = transform.position;
            vehicle.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

            //move the vehicle along its local axis
            float moveDistance = 0;
            if (FiftyFifty)
            {
                var leftX = leftBrake.position.x;
                var diffLeftX = Mathf.Abs(leftX - center);
                moveDistance = Range(0, diffLeftX);
            }
            {
                var rightX = rightBrake.position.x;
                var diffRightX = Mathf.Abs(rightX - center);
                moveDistance = Range(0, diffRightX);
            }
            vehicle.transform.Translate(moveDistance, 0, 0, Space.Self);

            //scale the vehicle
            if (CheckScale(minVehicleScale))
                vehicle.transform.localScale = Range(minVehicleScale, 1) * Vector3.one;
            else    
                vehicle.transform.localScale = .9f * Vector3.one;
        }
    }
}