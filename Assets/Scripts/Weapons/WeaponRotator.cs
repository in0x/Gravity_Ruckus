using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    Transform m_cameraTransform;

	void Start ()
    {
        m_cameraTransform = transform.root.gameObject.GetComponent<MovementHandler>().m_camera.transform;
    }
	
	void Update ()
    {
        transform.rotation = m_cameraTransform.rotation;
	}
}
