using CannonChallenge.Attributes;
using CannonChallenge.Events;
using UnityEngine;

namespace CannonChallenge.Cannon
{
    /// <summary>
    /// Controls cannon movement and activation
    /// </summary>
    public class CannonController : MonoBehaviour
    {

        [SerializeField, Expandable] private CannonDefinition _cannonDefinition;
        
        [SerializeField] private AttributeDTO _rotationSpeed;
        [SerializeField] private AttributeDTO _shotSpeed;
        [SerializeField] private AttributeDTO _areaEffect;
        
        [SerializeField] private float _smoothSpeed = 0.1f;
        [SerializeField] private GameObject _cannonballPrefab;
        [SerializeField] private Transform _shotSpot;
        [SerializeField] private bool _useSmoothDamp;

        [SerializeField] private CannonDataReference _cannonDataReference;
        
        [Header("Events")]
        [SerializeField] private MoveEventAsset _onMove;
        [SerializeField] private VoidEventAsset _onFire;

        private Vector2 _currentInput;
        private Vector2 _newInput;
        private Vector2 _smoothVelocity;

        private void OnEnable()
        {
            _currentInput = Vector2.zero;
            _onMove.OnInvoked.AddListener(OnMoveEvent);
            _onFire.OnInvoked.AddListener(OnFireEvent);
        }

        private void OnDisable()
        {
            _onMove.OnInvoked.RemoveListener(OnMoveEvent);
            _onFire.OnInvoked.RemoveListener(OnFireEvent);
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
            GameObject ball = Instantiate(_cannonballPrefab, _shotSpot.position, _shotSpot.rotation);
            ball.GetComponent<Rigidbody>().velocity = _shotSpot.transform.up * _shotSpeed.CurrentValue;
        }

        private void Update()
        {
            if (_newInput.magnitude < float.Epsilon)
            {
                return;
            }
            
            //Case smoothing transition is enabled
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
