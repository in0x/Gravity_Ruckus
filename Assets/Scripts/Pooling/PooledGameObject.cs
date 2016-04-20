using UnityEngine;

/*\
|*| This class defines a wrapper around GameObject instances that are managed by a pool.
|*| Access its instance via the Instance property. Once you are done using the Object,
|*| you need to call PooledGameObject::Release, which will reset all required properties
|*| of the instance and mark it as availible for other clients to use.
\*/
public class PooledGameObject
{
    GameObject m_instance;
    ResetComponent[] m_resetters;
    bool m_inUse;

    public PooledGameObject(GameObject instance)
    {
        m_instance = instance;
        m_resetters = m_instance.GetComponents<ResetComponent>();
        m_instance.SetActive(false);
    }

    public bool InUse
    {
        get { return m_inUse; }
        private set { }
    }

    public GameObject Instance
    {
        get { return m_instance; }
        private set { }
    }

    public virtual PooledGameObject Request()
    {
        if (m_inUse) return null;

        m_inUse = true;
        m_instance.SetActive(true);
        return this;
    }
    
    public virtual void Release()
    {
        if (!m_inUse) return;

        m_instance.SetActive(false);
        foreach (var resetter in m_resetters) resetter.Reset();
        m_inUse = false;
    }
}

