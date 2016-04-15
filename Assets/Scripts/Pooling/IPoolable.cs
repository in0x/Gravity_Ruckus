using UnityEngine;
using System.Collections;

/*\
|*| Represents the class for all objects that 
|*| can be allocated and managed by a pool.
\*/
public class Poolable 
{
    //P

    // Wether the object is currently being used 
    // by another object.
    public bool InUse { get; private set; }

    // Return control over the object back to the pool.
    // If any values need to be reset after usage it should
    // be done here
    void Release()
    {
        InUse = false;
    }
}
