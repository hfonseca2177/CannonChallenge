using CannonChallenge.Systems.Score;
using TMPro;
using UnityEngine;

namespace CannonChallenge.UI
{
    /// <summary>
    /// Panel to display the best score
    /// </summary>
    public class SummaryPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTxt;
        [SerializeField] private TextMeshProUGUI _hitsTxt;
        [SerializeField] private TextMeshProUGUI _shotsTxt;
        [Tooltip("Score Data serializer")]
        [SerializeField] private ScoreDAO _scoreDao;

        private LevelScore _bestScore;
        
        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            _bestScore = _scoreDao.Retrieve();
            if (_bestScore == null)
            {
                return;
            }
            _scoreTxt.text = _bestScore.Score.ToString();
            _hitsTxt.text = Mathf.FloorToInt(_bestScore.Accuracy).ToString();
            _shotsTxt.text = _bestScore.Shots.ToString();
        }
    }
}