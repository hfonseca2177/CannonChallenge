using CannonChallenge.Events;
using CannonChallenge.Systems.Score;
using CannonChallenge.Util;
using UnityEngine;

namespace CannonChallenge.Systems
{
    /// <summary>
    /// Score manager - controls the score registry
    /// </summary>
    public class ScoreSystem : MonoBehaviour
    {

        [Tooltip("when a new game started")]
        [SerializeField] private VoidEventAsset _onGameStart;
        [Tooltip("when game is over")]
        [SerializeField] private VoidEventAsset _onGameOver;
        [Tooltip("Score Data serializer")]
        [SerializeField] private ScoreDAO _scoreDao;
        [Tooltip("Load game scenes")]
        [SerializeField] private SceneLoader _sceneLoader;
        [Tooltip("Notifies whenever a score is awarded")]
        [SerializeField] private IntEventAsset _onScoreUpdateNotify;
        [Header("Score Award Events")]
        [SerializeField] private IntEventAsset _onBarrelDirectHit;
        [SerializeField] private IntEventAsset _onMultiHitExplosion;

        
        private LevelScore _bestScore;
        private LevelScore _currentScore;
        
        private void OnEnable()
        {
            _onGameStart.OnInvoked.AddListener(OnGameStartEvent);
            _onGameOver.OnInvoked.AddListener(OnGameOverEvent);
            _onBarrelDirectHit.OnInvoked.AddListener(OnBarrelHitEvent);
            _onMultiHitExplosion.OnInvoked.AddListener(OnBarrelHitEvent);
        }

        private void OnDisable()
        {
            _onGameStart.OnInvoked.RemoveListener(OnGameStartEvent);
            _onGameOver.OnInvoked.RemoveListener(OnGameOverEvent);
            _onBarrelDirectHit.OnInvoked.RemoveListener(OnBarrelHitEvent);
            _onMultiHitExplosion.OnInvoked.RemoveListener(OnBarrelHitEvent);   
        }

        private void OnBarrelHitEvent(int score)
        {
            _currentScore.Score += score;
            _onScoreUpdateNotify.Invoke(_currentScore.Score);
        }

        private void OnGameStartEvent()
        {
            LoadData();
            _currentScore = new LevelScore();
        }

        private void OnGameOverEvent()
        {
            SaveData();
            _sceneLoader.LoadSummary();
        }

        private void LoadData()
        {
            _bestScore = _scoreDao.Retrieve();
            if (_bestScore == null) ResetData();
        }
        
        private void ResetData()
        {
            _bestScore = new LevelScore();
            _scoreDao.Save(_bestScore);
        }
        
        private void SaveData()
        {
            _bestScore.UpdateRecords(_currentScore);
            _scoreDao.Save(_bestScore);
        }
        
        public void SkipToSummary()
        {
            SaveData();
            _sceneLoader.LoadSummary();
        }

    }
}