using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class jsonReader : MonoBehaviour
{
    void Start()
    {
        StreamReader file = new StreamReader(@"Assets/Scripts/test.json");
        string rawData = file.ReadToEnd();

        var json = JSON.Parse(rawData);

        Debug.Log(json["number"]);
    }

	void Update ()
    {
	
	}
}
