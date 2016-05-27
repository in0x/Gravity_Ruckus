using UnityEngine.SceneManagement;
using System.Collections.Generic;
using XboxCtrlrInput;
using System.Linq;

public class GameStartMenu : IPadGUIElement
{
    public MenuInputController m_globalInputController;

    // These are the fields for selecting player settings like character color or name
    public PlayerSelectionField m_topleft;
    public PlayerSelectionField m_topright;
    public PlayerSelectionField m_bottomleft;
    public PlayerSelectionField m_bottomright;

    public string m_sceneToLoad;

    List<PlayerSelectionField> m_playerFields;
    List<int> m_activatedControllers;
    CircularListIterator<PlayerSelectionField> m_iter;
    
    protected override void Start()
    {
        base.Start();

        m_playerFields = new List<PlayerSelectionField>();
        m_activatedControllers = new List<int>();
            
        m_playerFields.Add(m_topleft);
        m_playerFields.Add(m_topright);
        m_playerFields.Add(m_bottomleft);
        m_playerFields.Add(m_bottomright);

        m_iter = new CircularListIterator<PlayerSelectionField>(m_playerFields);
    }

    protected override void Update()
    {
        if (m_active)
        {
            // Foreach controller ID check if the player has signed on
            foreach (int id in Enumerable.Range(1, 4))
            {
                if (!m_activatedControllers.Contains(id)) CheckForSignIn(id);
                else CheckForSignOut(id);
            }
            
            if (m_globalInputController.GetButtonDown("Start"))
            {
                /* Set global game parameters and start game */
                for (int i = 0; i < m_activatedControllers.Count; i++)
                {
                    MatchProperties.playerControllerIDs[i] = m_activatedControllers[i];
                }

                MatchProperties.playerColors[0] = m_topleft.m_color;
                MatchProperties.playerColors[1] = m_topright.m_color;
                MatchProperties.playerColors[2] = m_bottomleft.m_color;
                MatchProperties.playerColors[3] = m_bottomright.m_color;
               
                SceneManager.LoadScene(m_sceneToLoad);
            }
        }
    }

    void CheckForSignIn(int id)
    {
        if (m_globalInputController.GetButtonDownWithID("ButtonClick", (XboxController)id))
        {
            if (m_iter.Current.m_active == false)
            {
                m_iter.Current.SetControllerNumber(id);
                m_iter.Current.Activate();
                m_iter++;
                m_activatedControllers.Add(id);
            }
        }
    }
    
    void CheckForSignOut(int id)
    {
        if (m_globalInputController.GetButtonDownWithID("Cancel", (XboxController)id))
        {
            var selectedField = m_playerFields.Find((field => {
                if (field.GetControllerNumber() == id) return true;
                return false;
            }));

            selectedField.SetControllerNumber(0);
            selectedField.Deactivate();
            m_activatedControllers.Remove(id);
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
