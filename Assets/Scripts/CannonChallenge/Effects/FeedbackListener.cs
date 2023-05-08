using System.Collections;
using CannonChallenge.Events;
using CannonChallenge.Util;
using Unity.VisualScripting;
using UnityEngine;

namespace CannonChallenge.VFX
{
    /// <summary>
    /// Feedback manager - plays a visual or sound effect based on a event  
    /// </summary>
    public class FeedbackListener : MonoBehaviour
    {
        [SerializeField] private AudioSource _sfx;
        [SerializeField] private ObjectPooling _vfxPooling;
        [SerializeField] private GameObjectEventAsset _onObjectFeedbackEvent;
        [SerializeField] private VoidEventAsset _onFeedbackEvent;
        [SerializeField] private float _duration;
        
        private bool _hasSfx;
        private bool _hasVfx;
        private WaitForSeconds _effectDuration;

        private void OnEnable()
        {
            _hasSfx = !_sfx.IsUnityNull();
            _hasVfx = !_vfxPooling.IsUnityNull();
            
            if (_onFeedbackEvent != null)
            {
                _onFeedbackEvent.OnInvoked.AddListener(OnFeedbackEvent);
            }
            if(_onObjectFeedbackEvent!= null)
            {
                _onObjectFeedbackEvent.OnInvoked.AddListener(OnFeedbackEvent);
            }
        }

        private void OnDisable()
        {
            if (_onFeedbackEvent != null)
            {
                _onFeedbackEvent.OnInvoked.RemoveListener(OnFeedbackEvent);
            }
            if (_onObjectFeedbackEvent != null)
            {
                _onObjectFeedbackEvent.OnInvoked.RemoveListener(OnFeedbackEvent);
            }
        }

        private void OnFeedbackEvent()
        {
            if(_hasSfx) PlaySound();
            if(_hasVfx) PlayVFX(transform.position);
        }

        private void OnFeedbackEvent(GameObject target)
        {
            if(_hasSfx) PlaySound();
            if(_hasVfx) PlayVFX(target.transform.position);
        }

        private void PlaySound()
        {
            _sfx.Play();
        }

        private void PlayVFX(Vector3 position)
        {
            var effect = _vfxPooling.Get();
            effect.transform.position = position;
            StartCoroutine(DisposeVFX(effect));
        }

        private IEnumerator DisposeVFX(GameObject vfx)
        {
            _effectDuration ??= new WaitForSeconds(_duration);
            yield return _effectDuration;
            _vfxPooling.Release(vfx);
        }

    }
}