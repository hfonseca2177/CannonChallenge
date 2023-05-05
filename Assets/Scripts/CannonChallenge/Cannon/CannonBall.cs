using System.Collections;
using CannonChallenge.Events;
using UnityEngine;

namespace CannonChallenge.Cannon
{
    /// <summary>
    /// Cannon ball projectile 
    /// </summary>
    public class CannonBall : MonoBehaviour
    {
        [SerializeField, Tooltip("Time before the projectile is destroyed")]
        protected float _lifeTime = 5f;

        [SerializeField, Tooltip("If should destroy after any impact")]
        protected bool _destroyOnImpact;
        
        [Tooltip("Returns enemy back to pool")]
        [SerializeField] private GameObjectEventAsset _onReleaseCannonballNotify;
        
        private Rigidbody _rigidBody;
        
        //accumulates the amount of time spawned
        private float _spawnTime;

        //used to control the realtime when it was actually launched and not instantiated
        private bool _launched;
        
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            if (!_launched)
            {
                return;
            }
            //accumulates the spawn time
            _spawnTime += Time.deltaTime;
            //if life time is gone destroy it
            if (_spawnTime > _lifeTime)
            {
                StartCoroutine(DestroyProjectile());
            }
        }
        
        //Starts projectile physics
        public void Fire(Vector3 velocity)
        {
            _rigidBody.velocity = velocity;
            _launched = true;
        }
        
        //case projectile collider are used as trigger
        private void OnTriggerEnter(Collider other)
        {
            //TODO possible implementations of splash projectiles or knock back
            
            if (_destroyOnImpact)
            {
                StartCoroutine(DestroyProjectile());
            }
        }
        
        IEnumerator DestroyProjectile()
        {
            yield return null;
            //reset variables that keep state
            _launched = false;
            _spawnTime = 0;
            //return object to pooling system
            _onReleaseCannonballNotify.Invoke(this.gameObject);
        }

    }
}