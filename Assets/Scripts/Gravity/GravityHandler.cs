using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityHandler : MonoBehaviour, IInputObserver
{
    public PlayerInput m_playerInputRef { get; set; }
    private Vector3 gravity;
    private Rigidbody rb;
    private Transform cam;
    private float rotationTime;
    private Quaternion newRotation;
    private Quaternion oldRotation;
    public float gravity_mult = 2.5f;
    private List<IGravityObserver> observers; 

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
        observers = new List<IGravityObserver>();
        observers.AddRange((GetComponentsInChildren<IGravityObserver>(true)));
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //if (PlayerInputRef.GetAxis("GravityVert")>0.1)
        Vector3 newGravity = Vector3.zero;
        Vector3 switchVector = Vector3.zero;
        if (m_playerInputRef.GetDPad("GravityUp"))
	    {
            newGravity = ReduceVector3(transform.forward) *gravity_mult;
            switchVector = new Vector3(-90, 0, 0);
	    }
        //else if (PlayerInputRef.GetAxis("GravityVert") < -0.1)
        else if (m_playerInputRef.GetDPad("GravityDown"))
        {
            newGravity = ReduceVector3(-transform.forward) * gravity_mult;
            switchVector = new Vector3(90, 0, 0);
        }
        //else if (PlayerInputRef.GetAxis("GravityHor") > 0.1)
        else if (m_playerInputRef.GetDPad("GravityRight"))
        {
            newGravity = ReduceVector3(transform.right) * gravity_mult;
            switchVector = new Vector3(0, 0, 90);
        }
        //else if (PlayerInputRef.GetAxis("GravityHor") < -0.1)
        else if (m_playerInputRef.GetDPad("GravityLeft"))
        {
            newGravity = ReduceVector3(-transform.right) * gravity_mult;
            switchVector = new Vector3(0, 0, -90);
        }
	    if (switchVector != Vector3.zero)
	    {
            rotationTime = 0;
            //print(transform.localRotation.eulerAngles);

            float newX = Mathf.Round(transform.localRotation.eulerAngles.x / 90) * 90;
            float newY = Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90;
            float newZ = Mathf.Round(transform.localRotation.eulerAngles.z / 90) * 90;

            transform.localRotation = Quaternion.Euler(newX, newY, newZ);
            newRotation = transform.localRotation * Quaternion.Euler(switchVector);
            oldRotation = transform.localRotation;
            gravity = newGravity;
            foreach (var observer in observers)
	        {
	            observer.GravitySwitch(newGravity);
	        }
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
