using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    Transform m_cameraTransform;

	void Start ()
    {
        foreach (Transform child in transform.parent.transform.parent.transform) 
        {
            if (child.tag == "MainCamera") m_cameraTransform = child;
        }   
    }
	
	void Update ()
    {
        transform.rotation = m_cameraTransform.rotation;
	}
}
