using UnityEngine;

namespace CannonChallenge.Attributes
{
    /// <summary>
    /// Attribute data definition - sets the baseline for the attributes that will be upgraded
    /// can also be used to create snapshots and thresholds. Eg. You could define a different pace from a
    /// specific upgrade level and jumps to another baseline 
    /// </summary>
    [CreateAssetMenu(fileName = "AttributeDef", menuName = "CannonChallenge/Attributes/Definition", order = 0)]
    public class AttributeDefinition : ScriptableObject
    {
        [Tooltip("Descriptive name for the attribute")]
        public string AttributeName;
        [Tooltip("Baseline value")]
        public float BaseLine;
        [Tooltip("Flag to check if there is a cap for the upgrades")]
        public bool HasCap;
        [Tooltip("Max value for the attribute throughout upgrades")]
        public float CapValue;
        [Tooltip("Semantic flag to indicate that it is a behavior modifier")]
        public bool IsTrait;
        [Tooltip("which level the attribute is unlocked and can be applied")]
        public int UnlockLevel;
        [Tooltip("Flat value to be added on each upgrade")]
        public float FlatModifier;
        [Tooltip("Growth percentage modifier to be applied on each upgrade")]
        public float PercentageModifier;
        [Tooltip("Icon that represents the attribute")]
        public Sprite Icon;
    }
}