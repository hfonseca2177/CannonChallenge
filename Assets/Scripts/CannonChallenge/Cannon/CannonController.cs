using CannonChallenge.Attributes;
using CannonChallenge.Events;
using CannonChallenge.Util;
using UnityEngine;

namespace CannonChallenge.Cannon
{
    /// <summary>
    /// Controls cannon movement and activation
    /// </summary>
    public class CannonController : MonoBehaviour
    {

        [Header("Scalable attributes")]
        [Tooltip("Cannon data definition")]
        [SerializeField, Expandable] private CannonDefinition _cannonDefinition;
        [Tooltip("Cannon data reference - this controller is responsible for keeping this reference")]
        [SerializeField] private CannonDataReference _cannonDataReference;
        [Tooltip("Position where the projectile will be spawned")]
        [SerializeField] private Transform _shotSpot;
        [Header("Damping movement option")]
        [Tooltip("Case smoothing transition is enabled")]
        [SerializeField] private bool _useSmoothDamp;
        [Tooltip("Smoothing damping modifier")]
        [SerializeField] private float _smoothSpeed = 0.1f;
        [Header("Cannonball Object pooling")]
        [Tooltip("Object Pooling reference")]
        [SerializeField] private ObjectPooling _cannonBallPool;
        [Tooltip("Projectile release event - return to pool")]
        [SerializeField] private GameObjectEventAsset _onCannonBallRelease;
        [Header("Events")]
        [SerializeField] private MoveEventAsset _onMove;
        [SerializeField] private VoidEventAsset _onFire;

        private AttributeDTO _rotationSpeed;
        private AttributeDTO _shotSpeed;
        private AttributeDTO _areaEffect;
        private Vector2 _currentInput;
        private Vector2 _newInput;
        private Vector2 _smoothVelocity;

        private void OnEnable()
        {
            _currentInput = Vector2.zero;
            _onMove.OnInvoked.AddListener(OnMoveEvent);
            _onFire.OnInvoked.AddListener(OnFireEvent);
            _onCannonBallRelease.OnInvoked.AddListener(OnCannonBallReleaseEvent);
        }

        private void OnDisable()
        {
            _onMove.OnInvoked.RemoveListener(OnMoveEvent);
            _onFire.OnInvoked.RemoveListener(OnFireEvent);
            _onCannonBallRelease.OnInvoked.RemoveListener(OnCannonBallReleaseEvent);
        }

        private void OnCannonBallReleaseEvent(GameObject cannonball)
        {
            _cannonBallPool.Release(cannonball);
        }

        private void Start()
        {
            LoadAttributes();
        }

        protected virtual void LoadAttributes()
        {
            _rotationSpeed = new AttributeDTO(_cannonDefinition.RotationSpeed);
            _shotSpeed = new AttributeDTO(_cannonDefinition.ShotSpeed);
            _areaEffect = new AttributeDTO(_cannonDefinition.AreaEffect);
            _cannonDataReference.RotationSpeed = _rotationSpeed;
            _cannonDataReference.ShotSpeed = _shotSpeed;
            _cannonDataReference.AreaEffect = _areaEffect;
        }

        private void OnFireEvent()
        {
            SpawnCannonball();
        }

        private void SpawnCannonball()
        {
            var ball = _cannonBallPool.Get();
            var ballTransform = ball.transform;
            ballTransform.position = _shotSpot.position;
            ballTransform.rotation = _shotSpot.rotation;
            Vector3 shotSpeed = _shotSpot.transform.up * _shotSpeed.CurrentValue;
            CannonBall cannonBall = ball.GetComponent<CannonBall>();
            cannonBall.Fire(shotSpeed);
            ball.SetActive(true);
        }

        private void Update()
        {
            if (_newInput.magnitude < float.Epsilon)
            {
                return;
            }
            if (_useSmoothDamp)
            {
                _currentInput = Vector2.SmoothDamp(_currentInput, _newInput, ref _smoothVelocity, _smoothSpeed);    
            }
            else
            {
                _currentInput = _newInput;
            }

            RotateHorizontally(_currentInput.x);
            RotateVertically(_currentInput.y);
        }

        private void OnMoveEvent(Vector2 input)
        {
            _newInput = input;
        }
        
        private void RotateHorizontally(float direction)
        {
            if (direction == 0)
            {
                return;
            }
            Vector3 newAngle = new Vector3(0, direction * _rotationSpeed.CurrentValue * Time.deltaTime, 0);
            RotateCannon(newAngle);
        }
        
        private void RotateVertically(float direction)
        {
            if (direction == 0)
            {
                return;
            }
            Vector3 newAngle = new Vector3(direction * _rotationSpeed.CurrentValue * Time.deltaTime, 0, 0);
            RotateCannon(newAngle);
        }

        private void RotateCannon(Vector3 newAngle)
        {
            Transform cannonTransform = transform;
            Quaternion rotation = cannonTransform.rotation;
            Quaternion endRotation = Quaternion.Euler(rotation.eulerAngles + newAngle);
            cannonTransform.rotation = endRotation;
        }
        
    }
}
