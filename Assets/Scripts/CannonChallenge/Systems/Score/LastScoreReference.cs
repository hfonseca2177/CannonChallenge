using UnityEngine;
namespace CannonChallenge.Systems.Score
{
    /// <summary>
    /// Reference for the last played level
    /// </summary>
    [CreateAssetMenu(fileName = "LastScoreReference", menuName = "CannonChallenge/Score/Last Score Reference")]
    public class LastScoreReference : ScriptableObject
    {
        public LevelReference LevelReference;
        public LevelScore BestScore;
        public LevelScore LastScore;
    }
}