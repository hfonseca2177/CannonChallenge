using CannonChallenge.Events;
using UnityEngine;

namespace CannonChallenge.Targets
{
    /// <summary>
    /// Extended Barrel Behavior
    /// </summary>
    public class ExplosiveBarrel: Barrel
    {
        [SerializeField, Expandable] private ExplosionDefinition _explosionDefinition;
        [SerializeField] private GameObjectEventAsset _onExplosionNotify;
        [SerializeField] private IntEventAsset _onMultiHitExplosionNotify;
        [SerializeField] private LayerMask _explosionMask;
        [Tooltip("when barrel is destroyed")]
        [SerializeField] private GameObjectEventAsset _onBarrelReleaseNotify;

        protected override void OnDirectHit(Collision collision)
        {
            Explosion(collision);
            base.OnDirectHit(collision);
        }

        /// <summary>
        /// Based on the intensity of the hit trigger a explosion that hits adjacent barrels
        /// </summary>
        /// <param name="collision"></param>
        private void Explosion(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > _explosionDefinition.Trigger)
            {
                Collider[] hits = new Collider[_explosionDefinition.HitCap];
                int size = Physics.OverlapSphereNonAlloc(transform.position, _explosionDefinition.Radius, hits, _explosionMask);
                if (size == 0)
                {
                    return;
                }
                foreach (var hit in hits)
                {
                    if (hit == null)
                    {
                        continue;
                    }
                    if (hit.TryGetComponent(out Rigidbody rigidBody))
                    {
                        rigidBody.AddExplosionForce(_explosionDefinition.Force, transform.position, _explosionDefinition.Radius);
                    }
                }
                _onMultiHitExplosionNotify.Invoke(size);
            }
            _onExplosionNotify.Invoke(this.gameObject);
            _onBarrelReleaseNotify.Invoke(this.gameObject);
        }
    }
}