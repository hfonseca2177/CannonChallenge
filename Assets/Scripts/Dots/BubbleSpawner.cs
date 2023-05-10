using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Dots
{
    /// <summary>
    /// Spawns bubbles in a cone from spawn point
    /// </summary>
    public class BubbleSpawner : MonoBehaviour
    {

        [SerializeField] private Transform _spawnPoint;        
        [SerializeField] [Range(1, 100)] private int _spawnsPerInterval = 20;        
        [SerializeField] [Range(0.1f, 2f)] private float _spawnInterval = 1f;
        [SerializeField] private GameObject _bubblePrefab;
        [SerializeField] private bool _spreadShot = true;

        private EntityManager _entityManager;
        private Entity _bubbleEntityPrefab;
        private float _cooldown;
        private BlobAssetStore _blobAssetStore;
        private bool _isActive = true;
        
        private void Start()
        {
            //instantiate blobasset which will serve as unmutable cache
            _blobAssetStore = new BlobAssetStore();
            //load ECS conversion settings
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
            //grabs the entityManager and set the blob asset to be used in the dyanmic instantiation
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _bubbleEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_bubblePrefab, settings);
        }

        private void Update()
        {
            if(!_isActive)
            {
                return;
            }

            _cooldown -= Time.deltaTime;

            if (!(_cooldown <= float.Epsilon))
            {
                return;
            }
            _cooldown += _spawnInterval;

            Vector3 position = _spawnPoint.position; 
            Vector3 rotation = _spawnPoint.rotation.eulerAngles;

            rotation.x = 0f;

            if (_spreadShot)
            {
                SpawnBubbleSpread(position, rotation);
            }
            else
            {
                SpawnBubble(position, rotation);
            }
            
        }

        private void SpawnBubble(Vector3 position, Vector3 rotation)
        {
            Entity bubble = _entityManager.Instantiate(_bubbleEntityPrefab);

            _entityManager.SetComponentData(bubble, new Translation { Value = position });
            _entityManager.SetComponentData(bubble, new Rotation { Value = Quaternion.Euler(rotation) });
        }

        private void SpawnBubbleSpread(Vector3 position, Vector3 rotation)
        {
            int max = _spawnsPerInterval / 2;
            int min = -max;
            int totalAmount = _spawnsPerInterval * _spawnsPerInterval;            
            Vector3 tempRot = rotation;
            int index = 0;

            NativeArray<Entity> bubbles = new NativeArray<Entity>(totalAmount, Allocator.TempJob);
            _entityManager.Instantiate(_bubbleEntityPrefab, bubbles);

            for (int x = min; x < max; x++)
            {
                tempRot.x = (rotation.x + 3 * x) % 360;

                for (int y = min; y < max; y++)
                {
                    tempRot.y = (rotation.y + 3 * y) % 360;

                    _entityManager.SetComponentData(bubbles[index], new Translation { Value = position });
                    _entityManager.SetComponentData(bubbles[index], new Rotation { Value = Quaternion.Euler(tempRot) });

                    index++;
                }
            }
            bubbles.Dispose();
        }

        public void StopSpawner()
        {
            _isActive = false;
        }

        private void OnDestroy()
        {
            _entityManager.DestroyAndResetAllEntities();
            _blobAssetStore.Dispose();
        }
    }
}
