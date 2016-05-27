using UnityEngine;
using System.Collections.Generic;

public class AimAssist : MonoBehaviour
{
    public float m_minDotToAdjust = 0.95f;
    public float m_strength = 0.4f;

    int m_numPlayers = 0;
    List<GameObject> m_players;
    Transform m_cameraTrafo;
    
    void Start()
    {
        m_players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        m_players.Remove(gameObject);
        m_numPlayers = m_players.Count;

        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "MainCamera") m_cameraTrafo = child;
        }
    }

    public void Adjust()
    {
        for (int i = 0; i < m_numPlayers; i++)
        {
            Vector3 vecToOther = (m_players[i].transform.position - gameObject.transform.position).normalized;
            Vector3 fwd = gameObject.transform.forward.normalized;

            if (Vector3.Dot(fwd, vecToOther) >= m_minDotToAdjust)
            {
                Quaternion rotation = gameObject.transform.localRotation * Quaternion.FromToRotation(fwd, vecToOther);
                rotation = Quaternion.Slerp(gameObject.transform.localRotation, rotation, m_strength);

                Quaternion temp = gameObject.transform.localRotation;
                temp.y = rotation.y;
                gameObject.transform.localRotation = temp;

                return;
            }
        }
    }
}
