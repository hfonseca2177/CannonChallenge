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
        [Tooltip("Notify when player fails objective")]
        [SerializeField] private VoidEventAsset _onObjectiveFail;
        
        private bool _gameOn;
        private float _elapsedTime;
        
        private void Start()
        {
            _onGameStartNotify.Invoke();
        }

        private void OnEnable()
        {
            _onObjectiveFail.OnInvoked.AddListener(OnObjectiveFailEvent);
        }

        private void OnDisable()
        {
            _onObjectiveFail.OnInvoked.RemoveListener(OnObjectiveFailEvent);
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