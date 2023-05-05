using CannonChallenge.Events;
using CannonChallenge.Levels.Island;
using CannonChallenge.Util;
using UnityEngine;

namespace CannonChallenge.Levels.Insland
{
    /// <summary>
    /// Boat spawner system
    /// </summary>
    public class BoatWaveSpawner : MonoBehaviour
    {
        [Header("Spawning options")]
        [Tooltip("Spawning position")]
        [SerializeField] private Transform _spawningPoint;
        [Tooltip("Boat object pooling reference")]
        [SerializeField] private ObjectPooling _boatObjectPooling;
        [Header("Events")]
        [Tooltip("when boat finishes its cross")]
        [SerializeField] private GameObjectEventAsset _onBoatRelease;
        [Tooltip("When a new wave starts")]
        [SerializeField] private IntEventAsset _onNewWave;

        private void OnEnable()
        {
            _onNewWave.OnInvoked.AddListener(OnNewWaveEvent);
            _onBoatRelease.OnInvoked.AddListener(OnBoatReleaseEvent);
        }

        private void OnDisable()
        {
            _onNewWave.OnInvoked.RemoveListener(OnNewWaveEvent);
            _onBoatRelease.OnInvoked.RemoveListener(OnBoatReleaseEvent);
        }

        private void OnNewWaveEvent(int waveNumber)
        {
            SpawnBoat();
        }

        private void OnBoatReleaseEvent(GameObject boat)
        {
            _boatObjectPooling.Release(boat);
        }

        private void SpawnBoat()
        {
            var boat = _boatObjectPooling.Get();
            var boatTransform = boat.transform;
            boatTransform.position = _spawningPoint.position;
            boatTransform.rotation = _spawningPoint.rotation;
            BoatController boatController = boat.GetComponent<BoatController>();
            boatController.InitSail();
            boat.SetActive(true);
        }
    }
 }
