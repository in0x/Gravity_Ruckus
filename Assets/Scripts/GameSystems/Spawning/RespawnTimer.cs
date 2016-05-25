using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RespawnTimer : MonoBehaviour
{
    public float m_timeToRespawn = 5f;

    Text m_text;
    bool m_readyToSpawn = true;
    float m_timeSinceDeath = 0f;

    public bool ReadyToSpawn
    {
        get { return m_readyToSpawn; }
        private set {}
    }

    void Start()
    {
        m_text = GetComponent<Text>();
    }

    void Update()
    {
        m_timeSinceDeath += Time.deltaTime;

        if (m_timeSinceDeath >= m_timeToRespawn)
        {
            m_readyToSpawn = true;
            m_timeSinceDeath = 0f;
            gameObject.SetActive(false);
        }
        else
        {
            m_text.text = "Respawning in: " + Mathf.Round((m_timeToRespawn - m_timeSinceDeath)).ToString();
        }
    }

    void OnEnable()
    {
        m_readyToSpawn = false;
        m_timeSinceDeath = 0f;
        m_text.text = "Respawning in: " + m_timeToRespawn.ToString();
    }
    
}
