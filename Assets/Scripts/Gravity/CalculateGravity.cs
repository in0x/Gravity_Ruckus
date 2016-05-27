using System;
using UnityEngine;

public class CalculateGravity : MonoBehaviour
{
	void FixedUpdate ()
	{
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 50);
	    Vector3 totalGravity = new Vector3(0,0,0);
        int i = 0;

        while (i < hitColliders.Length)
        {
            if (hitColliders[i] == gameObject.GetComponent<Collider>())
            {
                i++;
                continue;
            }

            Vector3 gravity = (hitColliders[i].ClosestPointOnBounds(gameObject.transform.position) - transform.position);

            if (Math.Abs(gravity.x) < 0.01) gravity.x = 0;
            if (Math.Abs(gravity.y) < 0.01) gravity.y = 0;            
            if (Math.Abs(gravity.z) < 0.01) gravity.z = 0;
            
            gravity.x = gravity.x == 0 ? 0 : 80 / gravity.x;
            gravity.y = gravity.y == 0 ? 0 : 80 / gravity.y;
            gravity.z = gravity.z == 0 ? 0 : 80 / gravity.z;

            gravity.x = Math.Min(Math.Max(gravity.x, -10), 10);
            gravity.y = Math.Min(Math.Max(gravity.y, -10), 10);
            gravity.z = Math.Min(Math.Max(gravity.z, -10), 10);

            gameObject.GetComponent<Rigidbody>().AddForce(gravity);
            totalGravity += gravity;

            i++;
        }
        transform.rotation = Quaternion.LookRotation(transform.forward, -totalGravity.normalized);
	}
}
