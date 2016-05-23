using UnityEngine;
using System.Collections.Generic;

public class AimAssistComponent : MonoBehaviour
{
    List<GameObject> players;

	void Start ()
    {
	    players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        players.Remove(gameObject);
    }
	
	void Update ()
    {
	    foreach (GameObject other in players)
        {
            Vector3 vecToOther = (other.transform.position - gameObject.transform.position).normalized;

            if (Vector3.Dot(gameObject.transform.forward.normalized, vecToOther) >= 0.99f)
            {
                Debug.Log("Looking close");
            }
        }
	}
}
