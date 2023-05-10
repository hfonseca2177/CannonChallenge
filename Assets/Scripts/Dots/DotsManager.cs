using CannonChallenge.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Dots
{
    /// <summary>
    /// Controls DOTs entity spawners
    /// </summary>
    public class DotsManager : MonoBehaviour
    {

        [SerializeField] private List<BarrelSpawner> _barrelSpawners;
        [SerializeField] private BubbleSpawner _bubbleSpawner;
        [SerializeField] private SceneLoader sceneLoader;

        public void DestroyEntitiesAndGoMenu()
        {
            _bubbleSpawner.StopSpawner();
            _barrelSpawners.ForEach(Destroy);
            Destroy(_bubbleSpawner);
            sceneLoader.LoadMenu();
        }
    }
}