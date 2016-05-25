using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    public string m_MenuSceneName = "";
    public float m_TimeBeforeLoadMenu = 10f;

    float m_fTimeSinceEnd = 0f;
    List<GameObject> m_canvasesGos;
    List<Camera> m_cameras;

    void Start()
    {
        m_canvasesGos = new List<GameObject>();
        m_cameras = new List<Camera>();

        Canvas[] canvae = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvae) m_canvasesGos.Add(canvas.gameObject);

        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach (Camera camera in cameras) m_cameras.Add(camera);
    }

    void Update()
    {
        m_fTimeSinceEnd += Time.deltaTime * 2f;

        // This is a quick hack to prevent other cameras turning back on due to the spawning system.
        foreach (GameObject canvas in m_canvasesGos) canvas.SetActive(false);

        if (m_fTimeSinceEnd >= m_TimeBeforeLoadMenu)
        {
            SceneManager.LoadScene(m_MenuSceneName);
        }
    }

    public void EndGame(List<Stats> playerStats)
    {
        Time.timeScale = 0.5f;
        foreach (GameObject canvas in m_canvasesGos) canvas.SetActive(false);

        foreach (Camera camera in m_cameras)
        {
            if (camera.gameObject.transform.parent.gameObject == playerStats[3].player)
            {
                camera.rect = new Rect(0f,0f,1f,1f);
            }
            else camera.gameObject.transform.parent.gameObject.SetActive(false);
        }

        // Reverse the order since the ScoreController does not sort the list properly for some reason
        for (int i = 0; i < 4; i++)
        {
            GameOverInformation.kills[3 - i] = playerStats[i].kills;
            GameOverInformation.deaths[3 - i] = playerStats[i].deaths;
            GameOverInformation.names[3 - i] = playerStats[i].player.name;
        }
    }
}
