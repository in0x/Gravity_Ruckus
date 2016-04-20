using UnityEngine;

/*\
|*| This component shall be included on all GameObject prefabs
|*| that need to be pooled. ResetComponent only guarantees that
|*| the objects position and rotation are reset. If any custom
|*| property needs to be reset, a new component derived from 
|*| ResetComponent shall be created to reset those properties. 
\*/
public class ResetComponent : MonoBehaviour
{
    public virtual void Reset()
    {
        gameObject.transform.position = new Vector3();
        gameObject.transform.rotation = Quaternion.identity;
    }
}
