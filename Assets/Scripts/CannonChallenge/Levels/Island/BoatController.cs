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
        [SerializeField] private BoatDefinition _boatDefinition;
        [Tooltip("Barrel object pooling reference")]
        [SerializeField] private ObjectPoolingReference _barrelObjectPoolingRef;
        [Tooltip("Barrel object pooling reference")]
        [SerializeField] private ObjectPoolingReference _explosiveBarrelObjectPoolingRef;
        [Tooltip("Barrel spawning points")]
        [SerializeField] private Transform[] _spawningPoints;
        [Header("Events")]
        [Tooltip("when boat finishes its cross")]
        [SerializeField] private GameObjectEventAsset _onBoatReleaseNotify;
        [Tooltip("when barrel is destroyed")]
        [SerializeField] private GameObjectEventAsset _onBarrelRelease;
        [Tooltip("when barrel is destroyed")]
        [SerializeField] private GameObjectEventAsset _onExplosiveBarrelRelease;

        private ObjectPooling _barrelObjectPooling;
        private ObjectPooling _explosiveBarrelObjectPooling;
        private List<GameObject> _barrels;
        private List<GameObject> _explosiveBarrels;
        private bool _isSailing;
        private float _speed;
        private Transform _target;

        private void OnEnable()
        {
            _onBarrelRelease.OnInvoked.AddListener(OnReleaseBarrelEvent);
            _onExplosiveBarrelRelease.OnInvoked.AddListener(OnExplosiveReleaseBarrelEvent);
        }

        private void OnDisable()
        {
            _onBarrelRelease.OnInvoked.RemoveListener(OnReleaseBarrelEvent);
            _onExplosiveBarrelRelease.OnInvoked.RemoveListener(OnExplosiveReleaseBarrelEvent);
        }

        
        private void OnExplosiveReleaseBarrelEvent(GameObject barrel)
        {
            _explosiveBarrels.Remove(barrel);
            _explosiveBarrelObjectPooling.Release(barrel);
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
            _speed = Random.Range(_boatDefinition.MinSpeed, _boatDefinition.MaxSpeed + 1);
        }

        private void SpawnBarrels()
        {   
            _barrelObjectPooling = _barrelObjectPoolingRef.Pool;
            _explosiveBarrelObjectPooling = _explosiveBarrelObjectPoolingRef.Pool;
            int barrelCount = Random.Range(_boatDefinition.MinNumBarrels, _boatDefinition.MaxNumBarrels + 1);
            int maxBarrels = Mathf.Clamp(barrelCount, _boatDefinition.MinNumBarrels, _spawningPoints.Length);
            _barrels = new List<GameObject>(maxBarrels);
            _explosiveBarrels = new List<GameObject>(maxBarrels);
            for (int i=0; i < maxBarrels; i++)
            {
                GameObject barrel;
                if (ShouldSpawnExplosiveByChance())
                {
                    barrel = _explosiveBarrelObjectPooling.Get();
                    _explosiveBarrels.Add(barrel);
                }
                else
                {
                    barrel = _barrelObjectPooling.Get();
                    _barrels.Add(barrel);
                }
                var spawnPoint = _spawningPoints[i];
                var barrelTransform = barrel.transform;
                barrelTransform.parent = transform;
                barrelTransform.position = spawnPoint.position;
                barrelTransform.rotation = spawnPoint.rotation;
                
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
            _isSailing = false;
            //release remaining barrels
            _barrels.ForEach(_barrelObjectPooling.Release);
            //release boat
            _onBoatReleaseNotify.Invoke(this.gameObject);
        }


        private bool ShouldSpawnExplosiveByChance()
        {
            return UnityEngine.Random.value < _boatDefinition.ExplosiveChance;
        }
    }
}