using UnityEngine;

public enum RotationType { y, z }

public class ScreenTransition : MonoBehaviour, ITransitioner
{
    public GameObject m_nextElement;
    public GameObject m_screenToSwitch;
    public RotationType m_rotType;

    Camera m_camera;

    IPadGUIElement m_next;
    IPadGUIElement m_self;

    Quaternion m_quatNewRot;

    float m_fAnimSpeed = 0.05f;

    void Start()
    {
        m_self = GetComponent<IPadGUIElement>();
        m_next = m_nextElement.GetComponent<IPadGUIElement>();
        m_camera = FindObjectOfType<Camera>();

        Vector3 fwd = m_camera.transform.forward;
       
        Vector3 newFwd = m_screenToSwitch.transform.position - m_camera.transform.position;
        Quaternion rot = Quaternion.FromToRotation(fwd.normalized, newFwd.normalized);
        m_quatNewRot = m_camera.transform.rotation * rot;

        if (m_rotType == RotationType.z)
        {
            m_quatNewRot = m_camera.transform.rotation * Quaternion.Euler(Vector3.Angle(fwd, newFwd), 0, 0);
        } 
    }

    public Transition Execute()
    {
        return delegate
        {
            while (m_camera.transform.rotation != m_quatNewRot)
            {
                m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, m_quatNewRot, m_fAnimSpeed);
                return null;
            }

            m_self.Deactivate();
            m_next.Activate();

            return m_next;
        };
    }

}
