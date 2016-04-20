using UnityEngine;
using System.Collections.Generic;

public class PoolTest : MonoBehaviour
{
    public GameObject prefab;

    List<PooledGameObject> objs = new List<PooledGameObject>();

    // Examplary use of the Pooling system
	void Start()
    {
        //var man = ObjectPoolManager.Get();

        //PooledGameObject instance;

        //for (int i = 0; i < 10; i++)
        //{
        //    instance = man.Request(prefab);
        //    objs.Add(instance);
        //}

        //Debug.Log(man.Request(prefab));

        //for (int i = 0; i < 10; i++) objs[i].Release();
        //objs.Clear();

        //for (int i = 0; i < 10; i++) Debug.Log(man.Request(prefab));
    }
}
