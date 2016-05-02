using UnityEngine;
using System.Collections;

public enum RotationType
{
    y,
    z
}

public class ScreenTransition : MonoBehaviour, ITransitioner
{
    public GameObject m_nextElement;
    public GameObject m_screenToSwitch;
    public RotationType rotType;

    Camera m_camera;

    IPadGUIElement m_next;
    IPadGUIElement m_self;

    Quaternion newRot;

    float m_animSpeed = 0.05f;

    void Start()
    {
        m_self = GetComponent<IPadGUIElement>();
        m_next = m_nextElement.GetComponent<IPadGUIElement>();
        m_camera = FindObjectOfType<Camera>();

        Vector3 fwd = m_camera.transform.forward;
       
        Vector3 newFwd = m_screenToSwitch.transform.position - m_camera.transform.position;
        Quaternion rot = Quaternion.FromToRotation(fwd.normalized, newFwd.normalized);
        newRot = m_camera.transform.rotation * rot;

        if (rotType == RotationType.z)
        {
            newRot = m_camera.transform.rotation * Quaternion.Euler(Vector3.Angle(fwd, newFwd), 0, 0);
        } 
    }

    public Transition Execute()
    {
        return delegate
        {
            while (m_camera.transform.rotation != newRot)
            {
                m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, newRot, m_animSpeed);
                return null;
            }

            m_self.Deactivate();
            m_next.Activate();

            return m_next;
        };
    }

}
