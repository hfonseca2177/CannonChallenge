using CannonChallenge.Events;
using UnityEngine;

namespace CannonChallenge.Levels.Island
{
    /// <summary>
    /// Controls waves in the level
    /// </summary>
    public class WaveSystem : MonoBehaviour
    {
        [Tooltip("Amount of waves to be spawned")]
        [SerializeField] private int _numberOfWaves;
        [Tooltip("Number of waves to wait for notification")]
        [SerializeField] private int _numberOfSpawners;
        [Header("Events")]
        [Tooltip("When game manager notifies level can start")]
        [SerializeField] private VoidEventAsset _onGameStart;
        [Tooltip("Notify to Spawners to start next wave")]
        [SerializeField] private IntEventAsset _onNewWaveNotify;
        [Tooltip("Spawner status notification")]
        [SerializeField] private VoidEventAsset _onSpawnerIdle;
        [Tooltip("When game is over event")]
        [SerializeField] private VoidEventAsset _onGameOver;
        [Tooltip("Notify that objective was successfully completed")]
        [SerializeField] private VoidEventAsset _onObjectiveSuccessNotify;
        [Tooltip("Notify message to player")]
        [SerializeField] private StringEventAsset _onNotificationNotify;
        
        private WavePhaseEnum _state = WavePhaseEnum.Disabled;
        private int _currentWaveIndex;
        private int _currentSpawnersIdle;
        
        private void OnEnable()
        {
            _onGameStart.OnInvoked.AddListener(OnGameStart);
            _onGameOver.OnInvoked.AddListener(OnGameOverEvent);
            _onSpawnerIdle.OnInvoked.AddListener(OnSpawnerIdleEvent);
        }

        private void OnDisable()
        {
            _onGameStart.OnInvoked.RemoveListener(OnGameStart);
            _onGameOver.OnInvoked.RemoveListener(OnGameOverEvent);
            _onSpawnerIdle.OnInvoked.RemoveListener(OnSpawnerIdleEvent);
        }

        private void OnSpawnerIdleEvent()
        {
            _currentSpawnersIdle++;
            if (_currentSpawnersIdle == _numberOfSpawners)
            {
                _state = WavePhaseEnum.Enabled;
            }
        }

        private void OnGameOverEvent()
        {
            _state = WavePhaseEnum.Disabled;
        }

        private void OnGameStart()
        {
            if (_state == WavePhaseEnum.Disabled)
            {
                _state = WavePhaseEnum.Enabled;
            }
        }

        private void Update()
        {
            if (_state != WavePhaseEnum.Enabled)
            {
                return;
            }
            SpawnWave();
        }

        private void SpawnWave()
        {   
            _currentWaveIndex++;
            if (_currentWaveIndex > _numberOfWaves)
            {
                _state = WavePhaseEnum.Disabled;
                _onObjectiveSuccessNotify.Invoke();
                return;
            }
            _state = WavePhaseEnum.OnCooldown;
            _currentSpawnersIdle = 0;
            _onNewWaveNotify.Invoke(_currentWaveIndex);
            _onNotificationNotify.Invoke($"Wave {_currentWaveIndex}");
        }

        public void Restart()
        {
            _currentSpawnersIdle = 0;
            _currentWaveIndex = 0;
            _state = WavePhaseEnum.Enabled;
        }
        
    }
}
