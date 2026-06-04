using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Interfaces;
using Project.Singleton;

namespace Project.Gameplay.Pooling
{
    public class PoolManager : MonoBehaviourSingleton<PoolManager>, IInitializable
    {
        public bool IsInitialized { get; private set; } // Used for bootstrap in future

        [SerializeField] private List<GameObject> objectsToPreInitialize;

        private Dictionary<GameObject, ObjectPool<GameObject>> objectPools; // Stores the Object Pools
        private Dictionary<GameObject, GameObject> instanceToPrefab; // Stores a clone Refernce to the original prefab

        private GameObject poolHolder;

        const int MIN_POOL_CAPACITY = 10;
        const int MAX_POOL_CAPACITY = 1000;

        protected override void OnAwake()
        {
            base.OnAwake();

            Init();
        }

        public void Init()
        {
            if(IsInitialized) return;

            Setup();

            PreInitializeObjectPool(objectsToPreInitialize, MIN_POOL_CAPACITY);

            objectsToPreInitialize = null;

            IsInitialized = true;
        }

        private void Setup()
        {
            objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
            instanceToPrefab = new Dictionary<GameObject, GameObject>();

            poolHolder = new GameObject("Object Pools");
            poolHolder.transform.SetParent(transform);
        }

        private void PreInitializeObjectPool(List<GameObject> prefabs, int count)
        {
            foreach (GameObject prefab in prefabs)
            {
                if(!objectPools.ContainsKey(prefab))
                {
                    CreatePool(prefab);
                }

                List<GameObject> temp = new(count);

                for(int i  = 0; i < count; i++)
                {
                    GameObject obj = objectPools[prefab].Get();
                    temp.Add(obj);
                }

                foreach(GameObject obj in temp)
                {
                    objectPools[prefab].Release(obj);
                }
            }
        }

        private void CreatePool(GameObject prefab)
        {
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => CreateObject(prefab),
                actionOnGet: OnGetObject,
                actionOnRelease: OnReleaseObject,
                actionOnDestroy: OnDestroyObject,
                collectionCheck: true, // Editor safety checks. Disable in release builds for performance.
                defaultCapacity: MIN_POOL_CAPACITY,
                maxSize: MAX_POOL_CAPACITY
                );

            objectPools[prefab] = pool;
        }

        private GameObject CreateObject(GameObject prefab)
        {
            GameObject obj = Instantiate(prefab);

            obj.transform.SetParent(poolHolder.transform);

            return obj;
        }

        private void OnGetObject(GameObject obj)
        {
            obj.GetComponent<IPoolable>()?.OnSpawn();
            obj.SetActive(true);
        }

        private void OnReleaseObject(GameObject obj)
        {
            obj.GetComponent<IPoolable>()?.OnDespawn();
            obj.SetActive(false);
        }

        private void OnDestroyObject(GameObject obj)
        {
            if (instanceToPrefab.ContainsKey(obj))
            {
                instanceToPrefab.Remove(obj);
            }

            Destroy(obj);
        }

        public GameObject Get(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, Transform parent = null)
        {
            if (objectToSpawn is null)
            {
                Debug.LogError("Tried to spawn null object");
                return null;
            }

            if(!objectPools.ContainsKey(objectToSpawn))
            {
                CreatePool(objectToSpawn);
            }

            GameObject obj = objectPools[objectToSpawn].Get();

            instanceToPrefab[obj] = objectToSpawn;

            obj.transform.SetParent(parent);
            obj.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
            obj.SetActive(true);

            return obj;
        }

        public T Get<T>(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, Transform parent = null) where T : Component
        {
            GameObject obj = Get(objectToSpawn, spawnPosition, spawnRotation, parent);

            if(obj.TryGetComponent(out T component))
            {
                return component;
            }

            Debug.LogError($"{objectToSpawn.name} does not contain component {typeof(T)}");
            return null;
        }

        public T Get<T>(T prefab, Vector3 spawnPosition, Quaternion spawnRotation, Transform parent = null) where T : Component
        {
            return Get<T>(prefab.gameObject, spawnPosition, spawnRotation, parent);
        }

        public void Release(GameObject obj)
        {
            if(instanceToPrefab.TryGetValue(obj, out GameObject prefab))
            {
                obj.transform.SetParent(poolHolder.transform);
                obj.SetActive(false);

                objectPools[prefab].Release(obj);
            }
            else
            {
                Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
            }
        }
    }
}