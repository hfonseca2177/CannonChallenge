using CannonChallenge.Events;
using System;
using UnityEngine;

namespace CannonChallenge.Player
{
    /// <summary>
    /// Game player Controller
    /// </summary>
    public class PlayerController : InputController
    {

        [SerializeField] private AudioSource _cheer;
        [SerializeField] private AudioSource[] _emotes;
        [SerializeField] private float _emoteChance;
        [SerializeField] private Animator _animator;
        [Header("Events")]
        [SerializeField] private IntEventAsset _onBarrelDirectHit;
        [SerializeField] private VoidEventAsset _onObjectiveSuccess;
        

        private void OnEnable()
        {
            _onBarrelDirectHit.OnInvoked.AddListener(OnBarrelHitEvent);
            _onObjectiveSuccess.OnInvoked.AddListener(OnObjectiveSuccessEvent);
        }

        private void OnDisable()
        {
            _onBarrelDirectHit.OnInvoked.RemoveListener(OnBarrelHitEvent);
            _onObjectiveSuccess.OnInvoked.RemoveListener(OnObjectiveSuccessEvent);
        }

        private void OnObjectiveSuccessEvent()
        {
            EmoteCheer();
        }

        private void OnBarrelHitEvent(int score)
        {
            if(ShouldEmoteByChance())
            {
                PlayEmote();
            }
        }

        private void PlayEmote()
        {
            //selective death animation
            int result = UnityEngine.Random.Range(0, _emotes.Length);
            _emotes[result].Play();
        }

        private bool ShouldEmoteByChance()
        {
            return UnityEngine.Random.value < _emoteChance;
        }

        private void EmoteCheer()
        {
            _animator.SetTrigger("Cheer");
        }


}
}
