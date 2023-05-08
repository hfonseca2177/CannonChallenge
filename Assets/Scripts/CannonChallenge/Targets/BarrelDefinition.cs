using UnityEngine;

namespace CannonChallenge.Targets
{
    /// <summary>
    /// Barrel data definition
    /// </summary>
    [CreateAssetMenu(fileName = "BarrelDefinition", menuName = "CannonChallenge/Target/Barrel Definition")]
    public class BarrelDefinition : ScriptableObject
    {
        public int Score;
        public float Mass;
        
    }
}