using UnityEngine;
using System.Collections.Generic;

public class GameStartController : MonoBehaviour
{
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;

    void Start ()
    {
        if (!MatchProperties.customGame)
        {
            MatchProperties.playerControllerIDs = new int[] { 1, 2, 3, 4 };
        }

        ConfigurePlayer(0, player0);
        ConfigurePlayer(1, player1);
        ConfigurePlayer(2, player2);
        ConfigurePlayer(3, player3);

        Time.timeScale = 1f;    
    }

    void ConfigurePlayer(int idx, GameObject player)
    {
        if (MatchProperties.playerControllerIDs[idx] != 0)
        {
            player.SetActive(true);
            player.GetComponentInChildren<PlayerInput>().m_playerNumber = MatchProperties.playerControllerIDs[idx];

            switch (MatchProperties.playerColors[idx])
            {
                case PlayerColor.blue:
                    player.GetComponentInChildren<Renderer>().material.color = new Color(0, 0, 1, 1);
                    break;

                case PlayerColor.green:
                    player.GetComponentInChildren<Renderer>().material.color = new Color(0, 1, 0, 1);
                    break;

                case PlayerColor.hotpink:
                    player.GetComponentInChildren<Renderer>().material.color = new Color(1, 0.0784f, 0.576f, 1);
                    break;

                case PlayerColor.red:
                    player.GetComponentInChildren<Renderer>().material.color = new Color(1, 0, 0, 1);
                    break;

                default:
                    break;
            }
        }
    }
}
