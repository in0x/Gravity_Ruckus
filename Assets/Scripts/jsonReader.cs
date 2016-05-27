using UnityEngine;
using System.IO;
using SimpleJSON;

public class jsonReader : MonoBehaviour
{
    void Start()
    {
        StreamReader file = new StreamReader(@"Assets/Scripts/test.json");
        string rawData = file.ReadToEnd();

        var json = JSON.Parse(rawData);
    }

	void Update ()
    {
	
	}
}
