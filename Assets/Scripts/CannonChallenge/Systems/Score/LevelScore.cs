using CannonChallenge.Serialization;

namespace CannonChallenge.Systems.Score
{
    /// <summary>
    /// Score data
    /// </summary>
    public class LevelScore: SerializableData
    { 
        public int Score;
        public int Accuracy;
        public int Shots;

        //compare each statistic with the most recent run and update the best one
        public void UpdateRecords(LevelScore recentScore)
        {
            if (recentScore.Score > Score) Score = recentScore.Score;
            if (recentScore.Accuracy > Accuracy) Accuracy = recentScore.Accuracy;
            if (recentScore.Shots > Shots) Shots = recentScore.Shots;
        }
    }
}