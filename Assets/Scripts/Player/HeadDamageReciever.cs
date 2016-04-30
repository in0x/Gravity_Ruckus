using UnityEngine;
using System.Collections;

public class HeadDamageReciever : MonoBehaviour, IDamageReciever
{
    public HealthController HealthController { get; set; }

    public void RecieveDamage(DamageInfo damageInfo)
    {
        Debug.Log("Head Dmg");
        damageInfo.bHeadshot = true;
        damageInfo.fDamage *= 2f;
        HealthController.ApplyDamage(damageInfo);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
