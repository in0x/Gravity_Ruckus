using UnityEngine;
using System.Collections.Generic;

/*\
|*| This class manages a pool of prefab instances. Clients
|*| should not instantiate this class manually, instead they
|*| shouls request an instance from the ObjectPoolManager,
|*| who holds all allocated ObjectPools. When requested, the pool
|*| will try to find an availible instance. If one is availible, 
|*| its enclosing PooledGameObject will be returned to you. If all
|*| instances are in use, null will be returned. 
\*/
public class ObjectPool 
{
    GameObject prefab;
    CircularListIterator<PooledGameObject> iter;
    List<PooledGameObject> instances;

    public int NumInstances
    {
        get { return instances.Count; }
        private set {}
    }

    // Allocates 8 instances if not specified otherwise.
    public ObjectPool(GameObject _prefab, int instancesToAllocate = 8)
    {
        prefab = _prefab;
        instances = new List<PooledGameObject>();
        iter = new CircularListIterator<PooledGameObject>(instances);
        Allocate(instancesToAllocate);
    }

    // Calls Instantiate() numObjects times, thereby creating that number
    // of GameObject instances.
    public void Allocate(int numObjects)
    {
        for (int count = 0; count < numObjects; count++)
        {
            instances.Add(new PooledGameObject(GameObject.Instantiate(prefab)));
        }
    }

    // O(n). Tries to find a free Instance. If all instances
    // are in use, null will be returned.
    public PooledGameObject Request()
    {
        PooledGameObject current = iter.Current;

        while (iter.Current.InUse)
        {
            iter++;
            if (iter.Current == current) return null;
        }

        return iter.Current.Request();
    }
}
