using UnityEngine;

namespace CannonChallenge.Levels.Moon
{
    /// <summary>
    /// Sets the engine physics gravity
    /// </summary>
    public class GravityManager : MonoBehaviour
    {
        [SerializeField] private Vector3 _gravity = new Vector3(0, -9.8f, 0);

        
        void Start()
        {
            Physics.gravity = _gravity;
        }

    }
}