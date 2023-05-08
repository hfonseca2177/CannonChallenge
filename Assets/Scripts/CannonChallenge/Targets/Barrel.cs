using CannonChallenge.Events;
using UnityEngine;

namespace CannonChallenge.Targets
{
    /// <summary>
    /// Barrel - cannon type target
    /// </summary>
    public class Barrel : MonoBehaviour
    {
        [SerializeField, Expandable] protected BarrelDefinition _barrelDefinition;
        [SerializeField] private IntEventAsset _onBarrelDirectHitNotify;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.mass = _barrelDefinition.Mass;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("CannonBall"))
            {
                OnDirectHit(collision);
            }
        }

        protected virtual void OnDirectHit(Collision collision)
        {
            _onBarrelDirectHitNotify.Invoke(_barrelDefinition.Score);
        }

    }
}