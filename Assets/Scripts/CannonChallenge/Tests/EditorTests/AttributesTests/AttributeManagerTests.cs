using CannonChallenge.Attributes;
using NUnit.Framework;
using UnityEngine;

namespace CannonChallenge.Tests.EditorTests.AttributesTests
{
    /// <summary>
    /// Tests Attribute manager logic 
    /// </summary>
    public class AttributeManagerTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AttributeManagerTestsSimplePasses()
        {
            //given
            float baseCost = 30f;
            int level = 1;
            float flatModifier = 0f;
            float percentageModifier = 0.05f;
            float expectedResult = 31.5f;
            //execute the upgrade calculation
            float result =AttributeManager.Instance.GetUpgradedValue(baseCost, level, flatModifier, percentageModifier);
            // Use the Assert class to test conditions
            Assert.True(Mathf.Approximately(result,expectedResult));
        }
    }
}
