using CannonChallenge.Attributes;
using UnityEngine;

namespace CannonChallenge.Cannon
{
    [CreateAssetMenu(fileName = "CannonDataReference", menuName = "CannonChallenge/Cannon/Data Reference", order = 0)]
    public class CannonDataReference : ScriptableObject
    {
        public AttributeDTO RotationSpeed;
        public AttributeDTO ShotSpeed;
        public AttributeDTO AreaEffect;
    }
}