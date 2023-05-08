using CannonChallenge.Events;
using UnityEngine;
using UnityEngine.UIElements;

namespace CannonChallenge.UI
{
    /// <summary>
    /// Game Score UI display  
    /// </summary>
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private IntEventAsset _onScoreUpdate;
        private UIDocument _scoreDocument;
        private Label _scoreValue;

        private void Awake()
        {
            _scoreDocument = GetComponent<UIDocument>();
            var root = _scoreDocument.rootVisualElement;
            _scoreValue = root.Q<Label>("ScoreValue");
            _scoreValue.text = "0";
        }

        private void OnEnable()
        {
            _onScoreUpdate.OnInvoked.AddListener(OnScoreUpdateEvent);
        }

        private void OnDisable()
        {
            _onScoreUpdate.OnInvoked.RemoveListener(OnScoreUpdateEvent);
        }

        private void OnScoreUpdateEvent(int score)
        {
            _scoreValue.text = score.ToString();
        }
    }
}