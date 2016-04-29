using UnityEngine;
using System.Collections;

public class WeaponRotator : MonoBehaviour
{
    Transform cameraTransform;

	void Start ()
    {
        // Is this real life.
        foreach (Transform child in transform.parent.transform.parent.transform) 
        {
            if (child.tag == "MainCamera") cameraTransform = child;
        }
        
    }
	
	void Update ()
    {
        transform.rotation = cameraTransform.rotation;
	}
}
