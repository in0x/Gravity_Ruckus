using UnityEngine;
using System.Collections;

public class ScreenTransition : MonoBehaviour, ITransitioner
{
    public GameObject m_nextElement;

    public GameObject m_screenToSwitch;
    public Camera m_camera;

    IPadGUIElement m_next;
    IPadGUIElement m_self;

    Vector3 fwd;
    Vector3 newFwd;
    Quaternion rot;
    Quaternion newRot;

    void Start()
    {
        m_self = GetComponent<IPadGUIElement>();
        m_next = m_nextElement.GetComponent<IPadGUIElement>();

        fwd = m_camera.transform.forward;
        newFwd = m_screenToSwitch.transform.position - m_camera.transform.position;
        rot = Quaternion.FromToRotation(fwd.normalized, newFwd.normalized);
        newRot = m_camera.transform.rotation * rot;
    }

    public Transition Execute()
    {
        return delegate
        {
            if (m_self.m_active)
            {
                m_self.Deactive();
                m_next.Activate();
            }
            
            float speed = 0.1f;

            while (m_camera.transform.rotation != newRot)
            {
                m_camera.transform.rotation = Quaternion.Slerp(rot, newRot, Time.time * speed);
                return null;
            }
            //m_camera.transform.rotation *= rot; 
            
            return m_next;
        };
    }

}
