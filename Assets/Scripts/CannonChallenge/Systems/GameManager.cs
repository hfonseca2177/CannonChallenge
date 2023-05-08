using System.Collections;
using CannonChallenge.Events;
using CannonChallenge.Util;
using UnityEngine;

namespace CannonChallenge.Systems
{
    /// <summary>
    /// Game Manager -controls core components and game cycle
    /// </summary>
    public class GameManager : MonoBehaviour
    {
 
        [Tooltip("Delay before start first wave")]
        [SerializeField] private float _delayToStartStages;
        [Header("Events")]
        [Tooltip("Notify a new game started")]
        [SerializeField] private VoidEventAsset _onGameStartNotify;
        [Tooltip("Notify when game is over")]
        [SerializeField] private VoidEventAsset _onGameOverNotify;
        [Tooltip("Notify when player achieves the objective")]
        [SerializeField] private VoidEventAsset _onObjectiveSuccess;
        [Tooltip("Notify when player fails objective")]
        [SerializeField] private VoidEventAsset _onObjectiveFail;
        [Tooltip("Notify message to player")]
        [SerializeField] private StringEventAsset _onNotificationNotify;
        [Tooltip("Scene Load helper")]
        [SerializeField] private SceneLoader _sceneLoader;

        private readonly WaitForSeconds _waitToLoadSummary = new(3);
        private bool _gameOn;
        private float _elapsedTime;

        private void OnEnable()
        {
            _onObjectiveSuccess.OnInvoked.AddListener(OnObjectiveSuccessEvent);
            _onObjectiveFail.OnInvoked.AddListener(OnObjectiveFailEvent);
        }

        private void OnDisable()
        {
            _onObjectiveSuccess.OnInvoked.RemoveListener(OnObjectiveSuccessEvent);
            _onObjectiveFail.OnInvoked.RemoveListener(OnObjectiveFailEvent);
        }

        private void OnObjectiveSuccessEvent()
        {
            _onNotificationNotify.Invoke("Level Completed!");
            _onGameOverNotify.Invoke();
            StartCoroutine(LoadSummary());
        }

        private void OnObjectiveFailEvent()
        {
            _onGameOverNotify.Invoke();
        }

        private void Update()
        {
            if (_gameOn) return;
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime < _delayToStartStages) return; 
            _gameOn = true;
            _onGameStartNotify.Invoke();
        }

        private IEnumerator LoadSummary()
        {
            yield return _waitToLoadSummary;
            _sceneLoader.LoadSummaryAdditive();
        }
    }
}