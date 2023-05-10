using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
namespace Dots
{
    /// <summary>
    /// Spawns a grid of barrel Targets
    /// </summary>
    public class BarrelSpawner : MonoBehaviour
    {

        [SerializeField] private int _rows;
        [SerializeField] private int _cols;
        [SerializeField] private float _spacing;
        [SerializeField] private GameObject _bubblePrefab;
        [SerializeField] private Transform _spawnPoint;

        private EntityManager _entityManager;
        private Entity _barrelEntityPrefab;
        private BlobAssetStore _blobAssetStore;

        private void Start()
        {
            _blobAssetStore = new BlobAssetStore();
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _barrelEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_bubblePrefab, settings);
            SpawnBarrelGrid();
        }

        private void SpawnBarrelGrid()
        {
            int totalAmount = _rows * _cols;
            int index = 0;
            //create an instance for all barrels
            NativeArray<Entity> targets = new NativeArray<Entity>(totalAmount, Allocator.TempJob);
            _entityManager.Instantiate(_barrelEntityPrefab, targets);
            //go through a matrix setting their position
            Vector3 spawnInitPosition = _spawnPoint.position; 
            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _cols; col++)
                {
                    Vector3 position = new Vector3(spawnInitPosition.x + col * _spacing, spawnInitPosition.y + row * _spacing, spawnInitPosition.z);
                    _entityManager.SetComponentData(targets[index], new Translation { Value = position });
                    _entityManager.SetComponentData(targets[index], new Rotation { Value = Quaternion.identity });
                    index++;
                }
            }
            targets.Dispose();
        }

        private void OnDestroy()
        {
            _entityManager.DestroyAndResetAllEntities();
            _blobAssetStore.Dispose();
        }
    }
}