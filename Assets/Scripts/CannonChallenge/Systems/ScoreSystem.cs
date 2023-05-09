using CannonChallenge.Events;
using CannonChallenge.Systems.Score;
using CannonChallenge.Util;
using System.Collections;
using UnityEngine;

namespace CannonChallenge.Systems
{
    /// <summary>
    /// Score manager - controls the score registry
    /// </summary>
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private ScoreDefinition _scoreDefinition;
        [Tooltip("reference to the current level")]
        [SerializeField] private LevelReference _levelReference;
        [Tooltip("Score Data serializer")]
        [SerializeField] private ScoreDAO _scoreDao;
        [Tooltip("Load game scenes")]
        [SerializeField] private SceneLoader _sceneLoader;
        [Tooltip("Level score reference - the system keeps this reference for last game played updated")]
        [SerializeField] private LastScoreReference _lastScoreReference;
        [Header("Events")]
        [Tooltip("when a new game started")]
        [SerializeField] private VoidEventAsset _onGameStart;
        [Tooltip("when game is over")]
        [SerializeField] private VoidEventAsset _onGameOver;
        [Tooltip("Notifies whenever a score is awarded")]
        [SerializeField] private IntEventAsset _onScoreUpdateNotify;
        [Header("Score Award Events")]
        [SerializeField] private IntEventAsset _onBarrelDirectHit;
        [SerializeField] private IntEventAsset _onMultiHitExplosion;
        [SerializeField] private VoidEventAsset _onBarrelInWater;
        [SerializeField] private VoidEventAsset _onFire;

        private readonly WaitForSeconds _waitToLoadSummary = new(3);
        private LevelScore _bestScore;
        private LevelScore _currentScore;
        
        private void OnEnable()
        {
            _onGameStart.OnInvoked.AddListener(OnGameStartEvent);
            _onGameOver.OnInvoked.AddListener(OnGameOverEvent);
            _onBarrelDirectHit.OnInvoked.AddListener(OnBarrelHitEvent);
            _onMultiHitExplosion.OnInvoked.AddListener(OnBarrelMultiHitEvent);
            _onBarrelInWater.OnInvoked.AddListener(OnBarrelInWaterEvent);
            _onFire.OnInvoked.AddListener(OnFireEvent);
        }

        private void OnDisable()
        {
            _onGameStart.OnInvoked.RemoveListener(OnGameStartEvent);
            _onGameOver.OnInvoked.RemoveListener(OnGameOverEvent);
            _onBarrelDirectHit.OnInvoked.RemoveListener(OnBarrelHitEvent);
            _onMultiHitExplosion.OnInvoked.RemoveListener(OnBarrelMultiHitEvent);
            _onBarrelInWater.OnInvoked.RemoveListener(OnBarrelInWaterEvent);
            _onFire.OnInvoked.RemoveListener(OnFireEvent);
        }

        private void OnFireEvent()
        {
            if(_currentScore!=null)
            {
                _currentScore.Shots++;
            }
        }

        private void OnBarrelInWaterEvent()
        {
            _currentScore.Score += _scoreDefinition.TargetOutBaseBonus;
            _onScoreUpdateNotify.Invoke(_currentScore.Score);
        }

        private void OnBarrelMultiHitEvent(int amount)
        {
            _currentScore.Score += amount * _scoreDefinition.MultiTargetBonus;
            _onScoreUpdateNotify.Invoke(_currentScore.Score);
        }

        private void OnBarrelHitEvent(int score)
        {
            _currentScore.Accuracy++;
            _currentScore.Score += score + _scoreDefinition.TargetDirectHitBonus;
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
            _lastScoreReference.LevelReference = _levelReference;
            _lastScoreReference.LastScore = _currentScore;
            _bestScore.UpdateRecords(_currentScore);
            _lastScoreReference.BestScore = _bestScore;
            _scoreDao.Save(_bestScore);
            StartCoroutine(LoadSummary());
        }

        private IEnumerator LoadSummary()
        {
            yield return _waitToLoadSummary;
            _sceneLoader.LoadSummaryAdditive();
        }
    }
}