using CannonChallenge.Events;
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
        [Tooltip("Level complete timeline reference")]
        [SerializeField] private GameObject _levelCompleteTimeline;

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
            _levelCompleteTimeline.SetActive(true);
        }

        public void OnTimelineOverSignal()
        {
            _levelCompleteTimeline.SetActive(false);
            _onGameOverNotify.Invoke();
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

    }
}