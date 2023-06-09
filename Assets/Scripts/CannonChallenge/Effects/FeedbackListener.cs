﻿using System.Collections;
using CannonChallenge.Events;
using CannonChallenge.Util;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

namespace CannonChallenge.Effects
{
    /// <summary>
    /// Feedback manager - plays a visual or sound effect based on a event  
    /// </summary>
    public class FeedbackListener : MonoBehaviour
    {
        [SerializeField] private AudioSource _sfx;
        [SerializeField] private ObjectPooling _vfxPooling;
        [SerializeField] private VisualEffect _visualEffect;
        [SerializeField] private GameObjectEventAsset _onObjectFeedbackEvent;
        [SerializeField] private VoidEventAsset _onFeedbackEvent;
        [SerializeField] private float _duration;
        
        private bool _hasSfx;
        private bool _hasVfx;
        private bool _hasOwnVfx;
        private WaitForSeconds _effectDuration;
      

        private void OnEnable()
        {
            _hasSfx = !_sfx.IsUnityNull();
            _hasVfx = !_vfxPooling.IsUnityNull();
            _hasOwnVfx = !_visualEffect.IsUnityNull();

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
            if (_hasOwnVfx) PlayNestedVfx();
        }

        private void OnFeedbackEvent(GameObject target)
        {
            Vector3 position = target.transform.position;
            if (_hasSfx) PlaySound();
            if(_hasVfx) PlayVFX(position);
            if (_hasOwnVfx) PlayNestedVfx(position);
        }

        private void PlaySound()
        {
            _sfx.Play();
        }

        private void PlayNestedVfx()
        {
            _visualEffect.Play();
        }

        private void PlayNestedVfx(Vector3 position)
        {
            _visualEffect.gameObject.transform.position = position;
            _visualEffect.Play();
        }

        private void PlayVFX(Vector3 position)
        {
            var effect = _vfxPooling.Get();
            effect.transform.position = position;
            effect.SetActive(true);
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