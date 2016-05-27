using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityHandler : MonoBehaviour, IInputObserver
{
    public PlayerInput m_playerInputRef { get; set; }
    public float m_fGravityMult = 2.5f;

    Vector3 m_gravityVec;
    Rigidbody m_rigidBody;
    Transform m_cameraTrafo;
    float m_fRotationTime;

    Quaternion m_quatNewRotation;
    Quaternion m_quatOldRotation;

    List<IGravityObserver> m_Observers;

    public Vector3 Gravity
    {
        get { return m_gravityVec; }
        set { m_gravityVec = value; }
    }
    
	void Start ()
	{
	    m_fRotationTime = 1;
	    m_rigidBody = GetComponent<Rigidbody>();
	    m_gravityVec = new Vector3(0, -1, 0)*m_fGravityMult;
	    m_cameraTrafo = transform.GetChild(0);
        m_Observers = new List<IGravityObserver>();
        m_Observers.AddRange((GetComponentsInChildren<IGravityObserver>(true)));
	}
	
	void Update ()
	{
        Vector3 newGravity = Vector3.zero;
        Vector3 switchVector = Vector3.zero;

        if (m_playerInputRef.GetDPad("GravityUp"))
	    {
            newGravity = ReduceVector3(transform.forward) *m_fGravityMult;
            switchVector = new Vector3(-90, 0, 0);
	    }
        else if (m_playerInputRef.GetDPad("GravityDown"))
        {
            newGravity = ReduceVector3(-transform.forward) * m_fGravityMult;
            switchVector = new Vector3(90, 0, 0);
        }
        else if (m_playerInputRef.GetDPad("GravityRight"))
        {
            newGravity = ReduceVector3(transform.right) * m_fGravityMult;
            switchVector = new Vector3(0, 0, 90);
        }
        else if (m_playerInputRef.GetDPad("GravityLeft"))
        {
            newGravity = ReduceVector3(-transform.right) * m_fGravityMult;
            switchVector = new Vector3(0, 0, -90);
        }
	    if (switchVector != Vector3.zero)
	    {
            m_fRotationTime = 0;
            
            float newX = Mathf.Round(transform.localRotation.eulerAngles.x / 90) * 90;
            float newY = Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90;
            float newZ = Mathf.Round(transform.localRotation.eulerAngles.z / 90) * 90;

            transform.localRotation = Quaternion.Euler(newX, newY, newZ);
            m_quatNewRotation = transform.localRotation * Quaternion.Euler(switchVector);
            m_quatOldRotation = transform.localRotation;

            m_gravityVec = newGravity;

            foreach (var observer in m_Observers)
	        {
	            observer.GravitySwitch(newGravity);
	        }
        }
    }

    void FixedUpdate()
    {
        m_rigidBody.AddForce(m_gravityVec*100, ForceMode.Acceleration);

        if (m_fRotationTime <= 1.1f)
        {
            transform.localRotation = Quaternion.Slerp(m_quatOldRotation, m_quatNewRotation, m_fRotationTime);
            m_fRotationTime += 0.1f;
        }
    }

    Vector3 ReduceVector3(Vector3 input)
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
