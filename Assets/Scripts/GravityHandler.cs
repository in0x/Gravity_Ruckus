using UnityEngine;
using System.Collections;

public class GravityHandler : MonoBehaviour
{
    private Vector3 gravity;
    private Rigidbody rb;
    private Transform cam;
    private float rotationTime;
    private Quaternion newRotation;
    private Quaternion oldRotation;

	// Use this for initialization
	void Start ()
	{
	    rotationTime = 1;
	    rb = GetComponent<Rigidbody>();
	    gravity = new Vector3(0, -1, 0);
	    cam = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("PAD1_B_BUTTON"))
	    {
            Vector3 newGravity = ReduceVector3(cam.forward);
	        if (gravity == newGravity)
	            return;
	        if ((gravity + newGravity) == Vector3.zero)
	        {
                rotationTime = 0;
	            newRotation = transform.localRotation*Quaternion.Euler(-180, 0, 0);
	        }
	        //rb.MoveRotation(rb.rotation*Quaternion.Euler(-180, 0, 0));
	        else
	        {
                rotationTime = 0;
                print(transform.localRotation.eulerAngles);
                float newX = Mathf.Round(transform.localRotation.eulerAngles.x / 90) * 90;
                float newY = Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90;
                float newZ = Mathf.Round(transform.localRotation.eulerAngles.z/90)*90;
                transform.localRotation = Quaternion.Euler(newX, newY,newZ  );
                newRotation = transform.localRotation * Quaternion.Euler(-90, 0, 0);
	        }
            //rb.MoveRotation(rb.rotation*Quaternion.Euler(-90, 0, 0));

            oldRotation = transform.localRotation;
	        gravity = newGravity;
	    }
	}

    void FixedUpdate()
    {
        rb.AddForce(gravity*100, ForceMode.Acceleration);
        if (rotationTime <= 1.1f)
        {
            transform.localRotation = Quaternion.Slerp(oldRotation, newRotation, rotationTime);
            rotationTime += 0.1f;
        }
        //else if (rotationTime < 10)
        //{
        //    transform.localRotation = Quaternion.Slerp(transform.localRotation, newRotation, rotationTime);
        //    rotationTime += 0.1f;
        //}
    }

    private Vector3 ReduceVector3(Vector3 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            if (Mathf.Abs(input.x) > Mathf.Abs(input.z))
                return new Vector3(Mathf.Sign(input.x) , 0 , 0);
            else
                return new Vector3(0 , 0 , Mathf.Sign(input.z));
        else
            if (Mathf.Abs(input.y) > Mathf.Abs(input.z))
                return new Vector3(0 , Mathf.Sign(input.y) , 0);
            else
                return new Vector3(0 , 0 , Mathf.Sign(input.z));

    }
}
