using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private Vector3 gravity;
    
    void Start()
    {
    }

    void Update()
    {
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public Vector3 GetGravity()
    {
        return gravity;
    }
}
