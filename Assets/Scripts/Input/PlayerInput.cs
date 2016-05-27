using UnityEngine;
using System;
using System.IO;
using SimpleJSON;
using XboxCtrlrInput;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
    public int m_playerNumber;
    
    //All = 0, First = 1, Second = 2, Third = 3, Fourth = 4
    XboxController m_controllerID;

    Dictionary<string, XboxAxis> m_axisBindings;
    Dictionary<string, XboxButton> m_buttonBindings;
    Dictionary<string, XboxDPad> m_dpadBindings;

    void Start ()
    {
        // Set reference to self for polling in all InputObservers
        foreach (var obs in GetComponents<IInputObserver>())
        {
            obs.m_playerInputRef = this;
        }

        m_axisBindings = new Dictionary<string, XboxAxis>();
        m_buttonBindings = new Dictionary<string, XboxButton>();
        m_dpadBindings = new Dictionary<string, XboxDPad>();

        // Get the abstract name to XboxCtrl Enum bindings for later usage
        using (StreamReader file = new StreamReader(@"Assets/JSON/XboxBindings.json"))
        {
            string rawData = file.ReadToEnd();
            JSONClass json = JSON.Parse(rawData) as JSONClass;

            foreach (var node in (json["Axis"].AsObject.m_Dict))
            {
                string valueStr = node.Value.ToString().Trim();
                valueStr = valueStr.Replace("\"", string.Empty).Replace("'", string.Empty);

                XboxAxis enumVal = (XboxAxis)Enum.Parse(typeof(XboxAxis), valueStr);
                m_axisBindings.Add(node.Key, enumVal);
            }
            
            foreach (var node in (json["DPad"].AsObject.m_Dict))
            {
                string valueStr = node.Value.ToString().Trim();
                valueStr = valueStr.Replace("\"", string.Empty).Replace("'", string.Empty);

                XboxDPad enumVal = (XboxDPad)Enum.Parse(typeof(XboxDPad), valueStr);
                m_dpadBindings.Add(node.Key, enumVal);
            }

            foreach (var node in (json["Buttons"].AsObject.m_Dict))
            {
                string valueStr = node.Value.ToString().Trim();
                valueStr = valueStr.Replace("\"", string.Empty).Replace("'", string.Empty);

                XboxButton enumVal = (XboxButton)Enum.Parse(typeof(XboxButton), valueStr);
                m_buttonBindings.Add(node.Key, enumVal);
            }
        }
        
        m_controllerID = (XboxController)m_playerNumber;     
    }
    
    public float GetAxis(string name)
    {
        return XCI.GetAxis(m_axisBindings[name], m_controllerID);
    }

    public bool GetButtonDown(string name)
    {
        return XCI.GetButtonDown(m_buttonBindings[name], m_controllerID);
    }

    public bool GetDPad(string name)
    {
        return XCI.GetDPadDown(m_dpadBindings[name], m_controllerID);
    }
}
