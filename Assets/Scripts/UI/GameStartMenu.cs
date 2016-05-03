using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using XboxCtrlrInput;
using System.Linq;

public class GameStartMenu : IPadGUIElement
{
    public MenuInputController m_globalInputController;

    // These are the fields for selecting player settings like character color or name
    public PlayerSelectionField topleft;
    public PlayerSelectionField topright;
    public PlayerSelectionField bottomleft;
    public PlayerSelectionField bottomright;

    public string m_sceneToLoad;

    List<PlayerSelectionField> playerFields;
    List<int> activatedControllers;
    CircularListIterator<PlayerSelectionField> iter;
    
    protected override void Start()
    {
        base.Start();

        playerFields = new List<PlayerSelectionField>();
        activatedControllers = new List<int>();
            
        playerFields.Add(topleft);
        playerFields.Add(topright);
        playerFields.Add(bottomleft);
        playerFields.Add(bottomright);

        iter = new CircularListIterator<PlayerSelectionField>(playerFields);
    }

    protected override void Update()
    {
        if (m_active)
        {
            // Foreach controller ID check if the player has signed on
            foreach (int id in Enumerable.Range(1, 4))
            {
                if (!activatedControllers.Contains(id)) CheckForSignIn(id);
                else CheckForSignOut(id);
            }
            
            if (m_globalInputController.GetButtonDown("Start"))
            {
                /* Set global game parameters and start game */
                for (int i = 0; i < activatedControllers.Count; i++)
                {
                    MatchProperties.playerControllerIDs[i] = activatedControllers[i];
                }

                MatchProperties.playerColors[0] = topleft.m_color;
                MatchProperties.playerColors[1] = topright.m_color;
                MatchProperties.playerColors[2] = bottomleft.m_color;
                MatchProperties.playerColors[3] = bottomright.m_color;
               
                SceneManager.LoadScene(m_sceneToLoad);
            }
        }
    }

    void CheckForSignIn(int id)
    {
        if (m_globalInputController.GetButtonDownWithID("ButtonClick", (XboxController)id))
        {
            if (iter.Current.m_active == false)
            {
                iter.Current.SetControllerNumber(id);
                iter.Current.Activate();
                iter++;
                activatedControllers.Add(id);
            }
        }
    }

    /*
        Needs to be tested with multiple controllers.
    */

    void CheckForSignOut(int id)
    {
        if (m_globalInputController.GetButtonDownWithID("Cancel", (XboxController)id))
        {
            var selectedField = playerFields.Find((field => {
                if (field.GetControllerNumber() == id) return true;
                return false;
            }));

            selectedField.SetControllerNumber(0);
            selectedField.Deactivate();
            activatedControllers.Remove(id);
        }
    }

    public override void Activate()
    {
        m_active = true;
        MatchProperties.playerControllerIDs = new int[]{ 0,0,0,0 };
        MatchProperties.customGame = true;
    }

    public override void Deactivate()
    {
        m_active = false;
        MatchProperties.customGame = false;
    }

}
