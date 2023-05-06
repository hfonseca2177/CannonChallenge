using System;
using CannonChallenge.Events;
using CannonChallenge.Util;
using UnityEngine;

namespace CannonChallenge.Levels.Island
{
    /// <summary>
    /// Boat spawner system
    /// </summary>
    public class BoatWaveSpawner : MonoBehaviour
    {
        [Header("Spawning options")]
        [Tooltip("Spawning position")]
        [SerializeField] private Transform _spawningPoint;
        [Tooltip("Sail direction target")]
        [SerializeField] private Transform _targetPoint;
        [Tooltip("Boat object pooling reference")]
        [SerializeField] private ObjectPooling _boatObjectPooling;
        [Header("Events")]
        [Tooltip("when boat finishes its cross")]
        [SerializeField] private GameObjectEventAsset _onBoatRelease;
        [Tooltip("When a new wave starts")]
        [SerializeField] private IntEventAsset _onNewWave;
        [Tooltip("Spawner status notification")]
        [SerializeField] private VoidEventAsset _onSpawnerIdleNotify;
        

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
            _onSpawnerIdleNotify.Invoke();
        }

        private void SpawnBoat()
        {
            var boat = _boatObjectPooling.Get();
            var boatTransform = boat.transform;
            boatTransform.position = _spawningPoint.position;
            boatTransform.rotation = _spawningPoint.rotation;
            BoatController boatController = boat.GetComponent<BoatController>();
            boat.SetActive(true);
            boatController.InitSail(_targetPoint);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;    
            Gizmos.DrawLine(_spawningPoint.position, _targetPoint.position);
        }
    }
 }
