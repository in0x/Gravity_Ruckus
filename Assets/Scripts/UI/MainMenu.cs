﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuickStartGame()
    {
        MatchProperties.customGame = false;
        SceneManager.LoadScene("complete_level");
    }
}
