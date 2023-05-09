using CannonChallenge.Events;
using CannonChallenge.Util;
using System.Collections.Generic;
using UnityEngine;

namespace CannonChallenge.Levels.Moon
{
    public class StageController : MonoBehaviour
    {
        [Tooltip("Random generate speed from this range")]
        [SerializeField] private StageControllerDefinition _stageConrollerDefinition;

        [Tooltip("Barrel object pooling")]
        [SerializeField] private ObjectPooling _barrelObjectPooling;
        [Tooltip("Explosive Barrel object pooling ")]
        [SerializeField] private ObjectPooling _explosiveBarrelObjectPooling;
        [Tooltip("Barrel spawning points")]
        [SerializeField] private Transform[] _spawningPoints;
        [Header("Events")]
        [Tooltip("When game manager notifies level can start")]
        [SerializeField] private VoidEventAsset _onGameStart;
        [Tooltip("When game is over event")]
        [SerializeField] private VoidEventAsset _onGameOver;
        [Tooltip("Notify to Spawners to start next wave")]
        [SerializeField] private IntEventAsset _onNewWaveNotify;
        [Tooltip("when barrel is destroyed")]
        [SerializeField] private GameObjectEventAsset _onBarrelRelease;
        [Tooltip("when barrel is destroyed")]
        [SerializeField] private GameObjectEventAsset _onExplosiveBarrelRelease;        
        [Tooltip("Notify that objective was successfully completed")]
        [SerializeField] private VoidEventAsset _onObjectiveSuccessNotify;
        [Tooltip("Notify message to player")]
        [SerializeField] private StringEventAsset _onNotificationNotify;

        private StageStateEnum _stageState = StageStateEnum.Disabled;
        private List<GameObject> _barrels;
        private List<GameObject> _explosiveBarrels;
        private int _currentStageIndex;
        private float _elapsedTime;        

        private void OnEnable()
        {
            _onGameStart.OnInvoked.AddListener(OnGameStart);
            _onGameOver.OnInvoked.AddListener(OnGameOverEvent);
            _onBarrelRelease.OnInvoked.AddListener(OnReleaseBarrelEvent);
            _onExplosiveBarrelRelease.OnInvoked.AddListener(OnExplosiveReleaseBarrelEvent);
        }

        private void OnDisable()
        {
            _onGameStart.OnInvoked.RemoveListener(OnGameStart);
            _onGameOver.OnInvoked.RemoveListener(OnGameOverEvent);
            _onBarrelRelease.OnInvoked.RemoveListener(OnReleaseBarrelEvent);
            _onExplosiveBarrelRelease.OnInvoked.RemoveListener(OnExplosiveReleaseBarrelEvent);
        }

        private void OnGameOverEvent()
        {
            _stageState = StageStateEnum.Disabled;
        }

        private void OnGameStart()
        {
            _stageState = StageStateEnum.Enabled;
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


        private void SpawnBarrels()
        {   
            int barrelCount = Random.Range(_stageConrollerDefinition.MinNumBarrels, _stageConrollerDefinition.MaxNumBarrels + 1);
            int maxBarrels = Mathf.Clamp(barrelCount, _stageConrollerDefinition.MinNumBarrels, _spawningPoints.Length);
            _barrels = new List<GameObject>(maxBarrels);
            _explosiveBarrels = new List<GameObject>(maxBarrels);
            for (int i = 0; i < maxBarrels; i++)
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
                var rb = barrelTransform.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                barrel.SetActive(true);
            }
        }

        private void Update()
        {
            if (_stageState == StageStateEnum.Disabled)
            {
                return;
            }
            if(_stageState == StageStateEnum.OnTimer && _elapsedTime < _stageConrollerDefinition.Duration)
            {
                _elapsedTime += Time.deltaTime;
                return;
            }
            _currentStageIndex++;
            if (_currentStageIndex > _stageConrollerDefinition.NumberOfStages)
            {
                _stageState = StageStateEnum.Disabled;
                _onObjectiveSuccessNotify.Invoke();
                return;
            }
            if(_currentStageIndex > 1)
            {
                ResetBarrels();
            }
            SpawnBarrels();
            _stageState = StageStateEnum.OnTimer;
            _elapsedTime = 0;
            _onNewWaveNotify.Invoke(_currentStageIndex);
            _onNotificationNotify.Invoke($"Stage {_currentStageIndex}");
        }

        public void ResetBarrels()
        {   
            _barrels.ForEach(_barrelObjectPooling.Release);
            _explosiveBarrels.ForEach(_explosiveBarrelObjectPooling.Release);
        }

        private bool ShouldSpawnExplosiveByChance()
        {
            return UnityEngine.Random.value < _stageConrollerDefinition.ExplosiveChance;
        }
    }
}