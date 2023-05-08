using CannonChallenge.Events;
using UnityEngine;
using Cinemachine;
using System.Collections;

namespace CannonChallenge.Effects
{
    /// <summary>
    /// Camera shake helper
    /// </summary>
    public class CameraShakeListener : MonoBehaviour
    {
        
        [SerializeField] private VoidEventAsset _onFeedbackEvent;
        [SerializeField] private float _lerpSpeed = 10;
        [SerializeField] private float _amplitude = 5f;
        [SerializeField] private float _frequency = 0.5f;
        [SerializeField] private float _duration = 0.2f;

        private CinemachineVirtualCamera _camera;
        private CinemachineBasicMultiChannelPerlin _noiseComponent;
        private Vector3 _cameraStartPosition;
        private Quaternion _cameraStartRotation;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _noiseComponent = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void OnEnable()
        {
            _onFeedbackEvent.OnInvoked.AddListener(OnFeedbackEvent);
        }

        private void OnDisable()
        {
            _onFeedbackEvent.OnInvoked.RemoveListener(OnFeedbackEvent);
        }


        private void OnFeedbackEvent()
        {
            ShakeCamera();
        }

        private void Start()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                _cameraStartRotation = Quaternion.identity;
            }
            else
            {
                _cameraStartRotation = mainCamera.transform.rotation;
            }
        }

        public void ShakeCamera()
        {
            _cameraStartPosition = _camera.transform.position;
            StartCoroutine(ShakeCameraCoroutine(_amplitude, _frequency, _duration));
        }

        private IEnumerator ShakeCameraCoroutine(float amplitude, float frequency, float duration)
        {
            _noiseComponent.m_FrequencyGain = frequency;
            _noiseComponent.m_AmplitudeGain = amplitude;
            yield return new WaitForSeconds(duration);
            _noiseComponent.m_FrequencyGain = 0;
            _noiseComponent.m_AmplitudeGain = 0;
            StartCoroutine(LerpToOriginalPosition(_lerpSpeed));
        }

        private IEnumerator LerpToOriginalPosition(float lerpSpeed)
        {
            var mainCamera = Camera.main;
            if (mainCamera == null) yield break;
            var mainCameraTransform = mainCamera.transform;
            float lerp = 0;
            while (lerp < 1)
            {
                lerp += Time.deltaTime * lerpSpeed;
                Quaternion currentRotation = Quaternion.Lerp(mainCameraTransform.rotation, _cameraStartRotation, lerp);
                Vector3 currentPosition = Vector3.Lerp(_camera.transform.position, _cameraStartPosition, lerp);
                _camera.transform.position = currentPosition;
                mainCameraTransform.rotation = currentRotation;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

    }
}