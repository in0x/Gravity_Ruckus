using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PlayerInput : MonoBehaviour
{
    MovementHandler mHand;

    Vector3 moveInput;
    Vector3 rotInput;

    bool jump = false;
    
    void Start ()
	{
        Cursor.lockState = CursorLockMode.Locked;
	    mHand = GetComponent<MovementHandler>();

        string[] padNames = Input.GetJoystickNames();

        foreach (string s in padNames) Debug.Log(s);

    }
	
	void Update ()
	{
	    float h = Input.GetAxis("Horizontal");
	    float v = Input.GetAxis("Vertical");

        float x = Input.GetAxisRaw("PAD1_Y_AXIS_HOR");
        float y = Input.GetAxisRaw("PAD1_Y_AXIS_VER");

        // Jump currently has no cooldown.
        if (Input.GetAxis("PAD1_LT") != 0)
        {
            jump = true;
        }
        
        moveInput = new Vector3(h,0,v);
        rotInput = new Vector3(x, y, 0);
	}

    void FixedUpdate()
    {   
        mHand.MoveInput(moveInput, rotInput, jump);
        jump = false;
    }
}
