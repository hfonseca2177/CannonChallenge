using CannonChallenge.Systems.Score;
using CannonChallenge.Util;
using TMPro;
using UnityEngine;

namespace CannonChallenge.UI
{
    /// <summary>
    /// Panel to display the best score
    /// </summary>
    public class SummaryPanel : MonoBehaviour
    {
        [Tooltip("Level score reference - reference for last game played")]
        [SerializeField] private LastScoreReference _lastScoreReference;
        [Tooltip("Load game scenes")]
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private TextMeshProUGUI _scoreTxt;
        [SerializeField] private TextMeshProUGUI _hitsTxt;
        [SerializeField] private TextMeshProUGUI _shotsTxt;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _recordColor;

        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            LevelScore last = _lastScoreReference.LastScore;
            LevelScore best = _lastScoreReference.BestScore;            
            SetScoreData(_scoreTxt, best.Score.ToString(), last.Score == best.Score);
            SetScoreData(_hitsTxt, best.Accuracy.ToString(), last.Accuracy == best.Accuracy);
            SetScoreData(_shotsTxt, best.Shots.ToString(), last.Shots == best.Shots);
        }

        private void SetScoreData(TextMeshProUGUI _field, string valueStr, bool bested)
        {
            _field.text = bested ? valueStr + " *" : valueStr;
            _field.color = bested ? _recordColor : _normalColor;
        }

        public void LoadLastLevel()
        {
            _sceneLoader.LoadSceneByIndex(_lastScoreReference.LevelReference.LevelIndex);
        }
    }
}