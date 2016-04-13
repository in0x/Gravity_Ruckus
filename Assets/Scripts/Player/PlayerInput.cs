using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using SimpleJSON;

public class PlayerInput : MonoBehaviour
{
    public int playerNumber = 1;

    MovementHandler mHand;


    private JSONNode json;

    void Start ()
    {
        foreach (var obs in GetComponents<IInputObserver>())
        {
            obs.PlayerInputRef = this;
        }

        StreamReader file = new StreamReader(@"Assets/Scripts/InputBindings.json");
        string rawData = file.ReadToEnd();

        json = JSON.Parse(rawData);

        //Cursor.lockState = CursorLockMode.Locked;
	    mHand = GetComponent<MovementHandler>();

        //string[] padNames = Input.GetJoystickNames();

        //foreach (string s in padNames) Debug.Log(s);

    }
	
	void Update ()
	{
	    
	}

    void FixedUpdate()
    {   
    }

    public float GetAxis(string name)
    {
        //if(name == "Jump")
        //Debug.Log(json[name] + playerNumber + " : "+ Input.GetAxis(json[name] + playerNumber));
        return Input.GetAxis(json[name]+playerNumber);
    }

    public bool GetButtonDown(string name)
    {
        return Input.GetButtonDown(json[name] + playerNumber);
    }

}
