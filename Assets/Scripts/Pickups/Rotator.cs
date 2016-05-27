using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 m_rotation;
    void Update ()
    {
        transform.Rotate(m_rotation * Time.deltaTime);
    }
}
