using CannonChallenge.Events;
using CannonChallenge.Util;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CannonChallenge.Levels.Island
{
    /// <summary>
    /// Boat Controller - controls the sail and barrels spawning
    /// </summary>
    public class BoatController : MonoBehaviour
    {
        [Tooltip("Random generate speed from this range")]
        [SerializeField] private Vector2 _moveSpeedRange;
        [Tooltip("Random generate amount of targets available in the boat")]
        [SerializeField] private Vector2 _targetRange;
        [Tooltip("Barrel object pooling reference")]
        [SerializeField] private ObjectPoolingReference _barrelObjectPoolingRef;
        [Tooltip("Barrel spawning points")]
        [SerializeField] private Transform[] _spawningPoints;
        [Header("Events")]
        [Tooltip("when boat finishes its cross")]
        [SerializeField] private GameObjectEventAsset _onBoatReleaseNotify;
        [Tooltip("when barrel is destroyed")]
        [SerializeField] private GameObjectEventAsset _onBarrelRelease;

        private ObjectPooling _barrelObjectPooling;
        private List<GameObject> _barrels;
        private bool _isSailing;
        private float _speed;
        private Transform _target;

        private void OnEnable()
        {
            _onBarrelRelease.OnInvoked.AddListener(OnReleaseBarrelEvent);
        }

        private void OnDisable()
        {
            _onBarrelRelease.OnInvoked.RemoveListener(OnReleaseBarrelEvent);
        }

        private void OnReleaseBarrelEvent(GameObject barrel)
        {
            _barrels.Remove(barrel);
            _barrelObjectPooling.Release(barrel);
        }

        public void InitSail(Transform target)
        {
            _target = target;    
            SpawnBarrels();
            SetSailSpeed();
            _isSailing = true;
        }

        private void SetSailSpeed()
        {
            _speed = Random.Range(_moveSpeedRange.x, _moveSpeedRange.y + 1);
        }

        private void SpawnBarrels()
        {   
            _barrelObjectPooling = _barrelObjectPoolingRef.Pool;
            int barrelCount = (int)Random.Range(_targetRange.x, _targetRange.y + 1);
            int maxBarrels = Mathf.Clamp(barrelCount, 1, _spawningPoints.Length);
            _barrels = new List<GameObject>(maxBarrels);
            for (int i=0; i < maxBarrels; i++)
            {
                var barrel = _barrelObjectPooling.Get();
                var spawnPoint = _spawningPoints[i];
                var barrelTransform = barrel.transform;
                barrelTransform.position = spawnPoint.position;
                //barrelTransform.rotation = spawnPoint.rotation;
                barrelTransform.parent = transform;
                _barrels.Add(barrel);
                barrel.SetActive(true);
            }
        }

        private void FixedUpdate()
        {
            if(!_isSailing)
            {
                return;
            }
            Sail();
        }

        private void Sail()
        {
            Transform boatTransform = transform;
            Vector3 newPosition = Vector3.MoveTowards(boatTransform.position, _target.position, _speed * Time.deltaTime);
            boatTransform.position = newPosition;
        }

        public void DeSpawnBoat()
        {
            Debug.Log("Release BOAT");    
            //release remaining barrels
            _barrels.ForEach(_barrelObjectPooling.Release);
            //release boat
            _onBoatReleaseNotify.Invoke(this.gameObject);
        }
    }
}