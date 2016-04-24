using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;

/*\
|*| This class is responsible for creation and managment of all object pools.
|*| Use it by accessing its singletion instance via ObjectPoolManager::Get().
|*| Which objects exist in pools is decided at startup, as defined in 
|*| prefabsToLoadPooling.json.
|*| Do not create ObjectPools by yourself, instead define it beforehand in the 
|*| JSON file.
\*/
public class ObjectPoolManager 
{
    Dictionary<GameObject, ObjectPool> m_objectPools;

    static ObjectPoolManager instance;
    
    // Ctor, reads the JSON setup file and creates a pool for each prefab
    // defined in it.
    ObjectPoolManager()
    {
        m_objectPools = new Dictionary<GameObject, ObjectPool>();

        using (StreamReader file = new StreamReader(@"Assets/JSON/prefabsToLoadPooling.json"))
        {
            string rawData = file.ReadToEnd();
            JSONClass json = JSON.Parse(rawData) as JSONClass;
            
            foreach (JSONArray node in json["Prefabs"].AsArray)
            {
                string path = node[0].ToString();
                int instances = node[1].AsInt;

                path = path.Replace("'", string.Empty).Replace("\"", string.Empty);
                
                GameObject prefab = (GameObject)Resources.Load(path, typeof(GameObject));

                if (instances == 0)
                {
                    m_objectPools.Add(prefab, (new ObjectPool(prefab)));
                }
                else
                {
                    m_objectPools.Add(prefab, (new ObjectPool(prefab, instances)));
                }
            }
        }
        
    }

    // Returns the singleton instance of ObjectPoolManager
    public static ObjectPoolManager Get()
    {
        if (instance == null)
        {
            instance = new ObjectPoolManager();
        }

        return instance;
    }

    ObjectPool GetPool(GameObject prefab)
    {
        ObjectPool pool;

        try
        {
             pool = m_objectPools[prefab];
        }
        catch (KeyNotFoundException)
        {
            pool = null;
        }

        return pool;
    }

    // Tries to return an instance of prefab from a pool. If a pool for the objects 
    // exists, the result of GameObjectPool::Request is returned. If no such pull exists,
    // null will be returned.
    public PooledGameObject Request(GameObject prefab)
    {
        var pool = GetPool(prefab);

        if (pool != null) return pool.Request();

        return null;
    }
    
    // Query wether a pool managing type prefab exists 
    public bool Exists(GameObject prefab)
    {
        var pool = GetPool(prefab);

        if (pool != null) return true;

        return false;
    }

    // Try to allocate numInstances of prefab. If a pool for prefab
    // exists, its capacity is increased. Otherwise a new pool 
    // of type prefab with numInstances is created.
    public void Allocate(GameObject prefab, int numInstances)
    {
        var pool = GetPool(prefab);

        if (pool != null) pool.Allocate(numInstances);

        else m_objectPools.Add(prefab, (new ObjectPool(prefab, numInstances)));
    }


    public int GetNumInstances(GameObject prefab)
    {
        var pool = GetPool(prefab);

        if (pool != null) return pool.NumInstances;

        return -1;
    }
}
