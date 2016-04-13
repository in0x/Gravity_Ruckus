using UnityEngine;
using System.Collections;

public class HeadDamageReciever : MonoBehaviour, IDamageReciever
{
    public HealthController HealthController { get; set; }

    public void RecieveDamage(float dmg)
    {
        Debug.Log("Head Dmg");
        HealthController.ApplyDamage(dmg * 2f);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
