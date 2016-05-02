using UnityEngine;
using System;
using System.IO;
using SimpleJSON;
using XboxCtrlrInput;
using System.Collections.Generic;

public class MenuInputController: MonoBehaviour
{
    public int playerNumber;

    //All = 0, First = 1, Second = 2, Third = 3, Fourth = 4
    XboxController controllerID;

    Dictionary<string, XboxAxis> axisBindings;
    Dictionary<string, XboxButton> buttonBindings;
    Dictionary<string, XboxDPad> dpadBindings;

    void Start()
    {
        // Set reference to self for polling in all InputObservers
        foreach (var obs in GetComponents<IInputObserver>())
        {
            //obs.PlayerInputRef = this;
        }

        axisBindings = new Dictionary<string, XboxAxis>();
        buttonBindings = new Dictionary<string, XboxButton>();
        dpadBindings = new Dictionary<string, XboxDPad>();

        // Get the abstract name to XboxCtrl Enum bindings for later usage
        using (StreamReader file = new StreamReader(@"Assets/JSON/XboxMenuBindings.json"))
        {
            string rawData = file.ReadToEnd();
            JSONClass json = JSON.Parse(rawData) as JSONClass;

            foreach (var node in (json["Axis"].AsObject.m_Dict))
            {
                string valueStr = node.Value.ToString().Trim();
                valueStr = valueStr.Replace("\"", string.Empty).Replace("'", string.Empty);

                XboxAxis enumVal = (XboxAxis)Enum.Parse(typeof(XboxAxis), valueStr);
                axisBindings.Add(node.Key, enumVal);
            }

            foreach (var node in (json["DPad"].AsObject.m_Dict))
            {
                string valueStr = node.Value.ToString().Trim();
                valueStr = valueStr.Replace("\"", string.Empty).Replace("'", string.Empty);

                XboxDPad enumVal = (XboxDPad)Enum.Parse(typeof(XboxDPad), valueStr);
                dpadBindings.Add(node.Key, enumVal);
            }

            foreach (var node in (json["Buttons"].AsObject.m_Dict))
            {
                string valueStr = node.Value.ToString().Trim();
                valueStr = valueStr.Replace("\"", string.Empty).Replace("'", string.Empty);

                XboxButton enumVal = (XboxButton)Enum.Parse(typeof(XboxButton), valueStr);
                buttonBindings.Add(node.Key, enumVal);
            }
        }

        controllerID = (XboxController)playerNumber;
    }

    public void SetPlayerNumber(int num)
    {
        controllerID = (XboxController)num;
    }

    public int GetPlayerNumber()
    {
        return (int)controllerID;
    }

    public float GetAxis(string name)
    {
        return XCI.GetAxis(axisBindings[name], controllerID);
    }

    public bool GetButtonDown(string name)
    {
        return XCI.GetButtonDown(buttonBindings[name], controllerID);
    }

    public bool GetButtonDownWithID(string name, XboxController id)
    {
        return XCI.GetButtonDown(buttonBindings[name], id);
    }

    public bool GetDPad(string name)
    {
        return XCI.GetDPadDown(dpadBindings[name], controllerID);
    }
}
