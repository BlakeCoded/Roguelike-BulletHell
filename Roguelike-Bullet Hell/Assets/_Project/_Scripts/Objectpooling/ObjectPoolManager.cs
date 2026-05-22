using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//public class ObjectPoolManager : MonoBehaviour
//{
//    [SerializeField] private bool _addToDontDestroyOnLoad = false;

//    private GameObject emptyHolder;

//    private static GameObject gameObjectEmpty;
//    private static GameObject particleSystemEmpty;

//    private static Dictionary<GameObject, ObjectPool<GameObject>> objectPools;
//    private static Dictionary<GameObject, GameObject> cloneToPrefabMap;

//    public enum PoolType
//    {
//        GameObjects,
//        ParticleSystem
//    }

//    public static PoolType PoolingType;

//    private void Awake()
//    {
//        objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
//        cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

//        SetupEmpties();
//    }

//    private void SetupEmpties()
//    {
//        emptyHolder = new GameObject("Object Pools");

//        gameObjectEmpty = new GameObject("GameObjects");
//        gameObjectEmpty.transform.SetParent(emptyHolder.transform);

//        particleSystemEmpty = new GameObject("Particle Effects");
//        particleSystemEmpty.transform.SetParent(emptyHolder.transform);

//        if (!_addToDontDestroyOnLoad)
//        {
//            // add object pools we dont want to destroy on load
//        }
//    }

//    private static void CreatePool(GameObject prefab, Vector3 position, Quaternion rotation, PoolType poolType = PoolType.GameObjects)
//    {
//        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
//            createFunc: () => CreateObject(prefab, position, rotation, poolType),
//            actionOnGet: OnGetObject,
//            actionOnRelease: OnReleaseObject,
//            actionOnDestroy: OnDestroyObject,
//            collectionCheck: false,
//            defaultCapacity: 20,
//            maxSize: 500
//            );

//        objectPools.Add(prefab, pool);
//    }

//    private static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation, PoolType poolType = PoolType.GameObjects)
//    {
//        prefab.SetActive(false);

//        GameObject obj = Instantiate(prefab, position, rotation);

//        prefab.SetActive(true);

//        GameObject parentObject = SetParentObject(poolType);
//        obj.transform.SetParent(parentObject.transform);

//        return obj;
//    }

//    private static void OnGetObject(GameObject obj)
//    {
//        // optional logic
//    }

//    private static void OnReleaseObject(GameObject obj)
//    {
//        obj.SetActive(false);
//    }

//    private static void OnDestroyObject(GameObject obj)
//    {
//        if(cloneToPrefabMap.ContainsKey(obj))
//        {
//            cloneToPrefabMap.Remove(obj);
//        }
//    }

//    private static GameObject SetParentObject(PoolType poolType)
//    {
//        switch(poolType)
//        {
//            case PoolType.GameObjects:
//                return gameObjectEmpty;

//            case PoolType.ParticleSystem:
//                return particleSystemEmpty;

//            default: return null;
//        }
//    }

//    private static T SpawnObject<T>(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects) where T : Object
//    {
//        if(!objectPools.ContainsKey(objectToSpawn))
//        {
//            CreatePool(objectToSpawn, spawnPosition, spawnRotation, poolType);
//        }

//        GameObject obj = objectPools[objectToSpawn].Get();

//        if(obj != null)
//        {
//            if(!cloneToPrefabMap.ContainsKey(obj))
//            {
//                cloneToPrefabMap.Add(obj, objectToSpawn);
//            }

//            obj.transform.position = spawnPosition;
//            obj.transform.rotation = spawnRotation;
//            obj.SetActive(true);

//            if(typeof(T) == typeof(GameObject))
//            {
//                return obj as T;
//            }

//            T componnet = obj.GetComponent<T>();

//            if(componnet == null)
//            {
//                Debug.LogError($"Object {objectToSpawn.name} doesn't have a component of type {typeof(T)}");
//                return null;
//            }

//            return componnet;
//        }

//        return null;
//    }

//    private static T SpawnObject<T>(T typePrefab, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects) where T : Component
//    {
//        return SpawnObject<T>(typePrefab.gameObject, spawnPosition, spawnRotation, poolType);
//    }

//    private static GameObject SpawnObject(GameObject objecToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects)
//    {
//        return SpawnObject<GameObject>(objecToSpawn, spawnPosition, spawnRotation, poolType);
//    }

//    public static void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.GameObjects)
//    {
//        if(cloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
//        {
//            GameObject parentObject = SetParentObject(poolType);

//            if(obj.transform.parent != parentObject.transform)
//            {
//                obj.transform.SetParent(parentObject.transform);
//            }

//            if(objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
//            {
//                pool.Release(obj);
//            }
//        }
//        else
//        {
//            Debug.LogWarning("Trying to return an object that is not pooled: " + obj.name);
//        }
//    }
//}
