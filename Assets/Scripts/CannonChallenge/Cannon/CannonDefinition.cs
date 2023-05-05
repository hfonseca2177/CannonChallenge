using CannonChallenge.Attributes;
using UnityEngine;

namespace CannonChallenge.Cannon
{
    /// <summary>
    /// Cannon data definition 
    /// </summary>
    [CreateAssetMenu(fileName = "CannonDefinition", menuName = "CannonChallenge/Cannon/Definition", order = 0)]
    public class CannonDefinition : ScriptableObject
    {
        public string Name;
        public GameObject CannonPrefab;
        public AttributeDefinition RotationSpeed;
        public AttributeDefinition ShotSpeed;
        public AttributeDefinition AreaEffect;
        
    }
}