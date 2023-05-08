using UnityEngine;

namespace CannonChallenge.Targets
{
    /// <summary>
    /// Explosion set attributes
    /// </summary>
    [CreateAssetMenu(fileName = "ExplosionDefinition", menuName = "CannonChallenge/Target/Explosion Definition")]
    public class ExplosionDefinition: ScriptableObject
    {
        public float Force;
        public float Radius;
        public float Trigger;
        public int HitCap;
    }
}