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
    Rigidbody m_rigidBody;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    public virtual void Reset()
    {
        gameObject.transform.position = new Vector3();
        gameObject.transform.rotation = Quaternion.identity;

        if (m_rigidBody != null)
        {
            m_rigidBody.velocity = Vector3.zero;
            m_rigidBody.angularVelocity = Vector3.zero;
        }
    }
}
