using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string m_sceneToLoad;

    public void Load()
    {
        SceneManager.LoadScene(m_sceneToLoad);
    }
            
}
