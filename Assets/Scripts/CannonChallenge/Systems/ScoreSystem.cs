using CannonChallenge.Events;
using UnityEngine;

namespace CannonChallenge.Systems
{
    /// <summary>
    /// Score manager - controls the score registry
    /// </summary>
    public class ScoreSystem : MonoBehaviour
    {

        [Tooltip("when a new game started")]
        [SerializeField] private VoidEventAsset _onGameStart;
        [Tooltip("when game is over")]
        [SerializeField] private VoidEventAsset _onGameOver;



    }
}