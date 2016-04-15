using UnityEngine;
using System.Collections.Generic;

public class ObjectPool
{
    CircularListIterator<Poolable> iter;
    List<Poolable> instances;
    public ObjectPool()
    {
        instances = new List<Poolable>();
        iter = new CircularListIterator<Poolable>(instances);
    }

    public void Allocate(int numObjects)
    {
        for (int count = 0; count < numObjects; count++)
        {
            instances.Add(new Poolable());
        }
    }

    public Poolable Request()
    {
        Poolable current = iter.Current;

        while (iter.Current.InUse)
        {
            iter++;

            if (iter.Current == current) return null;
        }

        Poolable retVal = iter.Current;
        
        return retVal;
    }
}
