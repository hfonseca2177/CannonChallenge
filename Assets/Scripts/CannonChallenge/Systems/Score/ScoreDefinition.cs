using UnityEngine;

namespace CannonChallenge.Systems.Score
{
    [CreateAssetMenu(fileName = "ScoreDefinition", menuName = "CannonChallenge/Systems/Score Definition", order = 0)]
    public class ScoreDefinition : ScriptableObject
    {
        public int TargetOutBaseBonus;
        public int MultiTargetBonus;
        public int TargetDirectHitBonus;
    }
}