using UnityEngine;

namespace CannonChallenge.Attributes
{
    /// <summary>
    /// Singleton Helper class to Separate core calculation update logic from the system
    /// Spreadsheet Simulation and documentation at: https://docs.google.com/spreadsheets/d/1hhO7k8r7R0Ur-jKHlVEQ-AG9sdsMTIOnARan3C8XZHQ/edit?usp=sharing
    /// </summary>
    public class AttributeManager
    {
        private static AttributeManager _instance;
        public static AttributeManager Instance
        {
            get { return _instance ??= new AttributeManager(); }
        }
        
        private AttributeManager()
        {
        }
        
        public float GetUpgradedValue(float baseCost, int level, float flatModifier, float percentageModifier)
        {
            return (baseCost * Mathf.Pow((1 + percentageModifier),level)) + (flatModifier * level);
        }
    }
}