using UnityEngine;
using System.Collections;

public class CreateGameObject : MonoBehaviour
{

    public GameObject objToCreate;

	// Use this for initialization
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 50), "Create"))
        {
            GameObject.Instantiate(objToCreate);
        }
    }
}
