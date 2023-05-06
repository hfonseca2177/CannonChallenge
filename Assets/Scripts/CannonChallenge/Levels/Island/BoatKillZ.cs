using UnityEngine;

namespace CannonChallenge.Levels.Island
{
    /// <summary>
    /// End zone for boats
    /// </summary>
    public class BoatKillZ : MonoBehaviour
    {      
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out BoatController boatController))
            {
                boatController.DeSpawnBoat();
            }
        }
    }
}