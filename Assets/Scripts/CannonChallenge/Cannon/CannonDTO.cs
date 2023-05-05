using System;

namespace CannonChallenge.Cannon
{
    /// <summary>
    /// Cannon Data Transfer Object - Transient values of cannon attributes
    /// </summary>
    [Serializable]
    public class CannonDTO
    {
        public float RotationSpeed;
        private float ShootSpeed;
        private float AreaEffect;
    }
}