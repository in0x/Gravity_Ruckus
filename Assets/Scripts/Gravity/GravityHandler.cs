using UnityEngine;
using System.Collections;

public class GravityHandler : MonoBehaviour, IInputObserver
{
    public PlayerInput PlayerInputRef { get; set; }
    private Vector3 gravity;
    private Rigidbody rb;
    private Transform cam;
    private float rotationTime;
    private Quaternion newRotation;
    private Quaternion oldRotation;
    public float gravity_mult = 2.5f;

    // This is used for spawning in non-default rotations
    public Vector3 Gravity
    {
        get
        {
            return gravity;
        }
        set
        {
            // We can do validation here
            gravity = value;
        }
    }

	// Use this for initialization
	void Start ()
	{
	    rotationTime = 1;
	    rb = GetComponent<Rigidbody>();
	    gravity = new Vector3(0, -1, 0)*gravity_mult;
	    cam = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //if (PlayerInputRef.GetAxis("GravityVert")>0.1)
        if (PlayerInputRef.GetDPad("GravityUp"))
	    {
            Vector3 newGravity = ReduceVector3(transform.forward) *gravity_mult;
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
        //else if (PlayerInputRef.GetAxis("GravityVert") < -0.1)
        if (PlayerInputRef.GetDPad("GravityDown"))
        {
            Vector3 newGravity = ReduceVector3(-transform.forward) * gravity_mult;
            {
                rotationTime = 0;
                print(transform.localRotation.eulerAngles);
                float newX = Mathf.Round(transform.localRotation.eulerAngles.x / 90) * 90;
                float newY = Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90;
                float newZ = Mathf.Round(transform.localRotation.eulerAngles.z / 90) * 90;
                transform.localRotation = Quaternion.Euler(newX, newY, newZ);
                newRotation = transform.localRotation * Quaternion.Euler(90, 0, 0);
            }
            //rb.MoveRotation(rb.rotation*Quaternion.Euler(-90, 0, 0));

            oldRotation = transform.localRotation;
            gravity = newGravity;
        }
        //else if (PlayerInputRef.GetAxis("GravityHor") > 0.1)
        if (PlayerInputRef.GetDPad("GravityRight"))
        {
            Vector3 newGravity = ReduceVector3(transform.right) * gravity_mult;
            {
                rotationTime = 0;
                print(transform.localRotation.eulerAngles);
                float newX = Mathf.Round(transform.localRotation.eulerAngles.x / 90) * 90;
                float newY = Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90;
                float newZ = Mathf.Round(transform.localRotation.eulerAngles.z / 90) * 90;
                transform.localRotation = Quaternion.Euler(newX, newY, newZ);
                newRotation = transform.localRotation * Quaternion.Euler(0, 0, 90);
            }
            //rb.MoveRotation(rb.rotation*Quaternion.Euler(-90, 0, 0));

            oldRotation = transform.localRotation;
            gravity = newGravity;
        }
        //else if (PlayerInputRef.GetAxis("GravityHor") < -0.1)
        if (PlayerInputRef.GetDPad("GravityLeft"))
        {
            Vector3 newGravity = ReduceVector3(-transform.right) * gravity_mult;
            {
                rotationTime = 0;
                print(transform.localRotation.eulerAngles);
                float newX = Mathf.Round(transform.localRotation.eulerAngles.x / 90) * 90;
                float newY = Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90;
                float newZ = Mathf.Round(transform.localRotation.eulerAngles.z / 90) * 90;
                transform.localRotation = Quaternion.Euler(newX, newY, newZ);
                newRotation = transform.localRotation * Quaternion.Euler(0, 0, -90);
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
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.z))
            {
                return new Vector3(Mathf.Sign(input.x), 0, 0);
            }
            else
            {
                return new Vector3(0, 0, Mathf.Sign(input.z));
            }
        }
        else
        {
            if (Mathf.Abs(input.y) > Mathf.Abs(input.z))
            {
                return new Vector3(0, Mathf.Sign(input.y), 0);
            }
            else
            {
                return new Vector3(0, 0, Mathf.Sign(input.z));
            }
        }

    }
}
