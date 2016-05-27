using UnityEngine;
using System.Linq;
using System.Collections.Generic;

class DotCollisionInfo
{
    public DotCollisionInfo(GameObject _gameObject, float _timeSinceLast)
    {
        gameObject = _gameObject;
        m_fTimeSinceLastTick = _timeSinceLast;
        m_bTouched = true;
    }

    public GameObject gameObject;
    public float m_fTimeSinceLastTick;
    public bool m_bTouched;
}

public class DamageOnTouch : MonoBehaviour
{
    public float m_damage;
    public float m_timeBetweenTicks;

    Dictionary<GameObject, DotCollisionInfo> m_collisions;

    void Start()
    {
        m_collisions = new Dictionary<GameObject, DotCollisionInfo>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject.CompareTag("Player") && !m_collisions.ContainsKey(collision.gameObject))
        {
            Debug.Log(collision.gameObject.transform.root.gameObject.tag);
            DotCollisionInfo info = new DotCollisionInfo(collision.gameObject, m_timeBetweenTicks);
            m_collisions.Add(info.gameObject, info);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject.CompareTag("Player") && m_collisions.ContainsKey(collision.gameObject))
        {
            
            DotCollisionInfo info = m_collisions[collision.gameObject];
            info.m_bTouched = true;

            Debug.Log("stay");
            if (info.m_fTimeSinceLastTick <= 0)
            {
                
                info.gameObject.SendMessageUpwards("ApplyDamage", new DamageInfo(gameObject, m_damage, false, true), SendMessageOptions.DontRequireReceiver);
                info.m_fTimeSinceLastTick = m_timeBetweenTicks;
            }
            else
            {
                Debug.Log(info.m_fTimeSinceLastTick + ":      " + Time.fixedDeltaTime);
                info.m_fTimeSinceLastTick -= Time.fixedDeltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        var toRemove = m_collisions.Where(pair => pair.Value.m_bTouched == false)
                         .Select(pair => pair.Key)
                         .ToList();

        foreach (var key in toRemove) m_collisions.Remove(key);
        foreach (var pair in m_collisions) pair.Value.m_bTouched = false;
    }
}