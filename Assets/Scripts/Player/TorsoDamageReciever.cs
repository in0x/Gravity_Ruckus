using UnityEngine;
using System.Collections;

public class TorsoDamageReciever : MonoBehaviour, IDamageReciever
{

    public HealthController HealthController { get; set; }

    public void RecieveDamage(DamageInfo damageInfo)
    {
        Debug.Log("Torso dmg");
        damageInfo.fDamage *= 1f;
        HealthController.ApplyDamage(damageInfo);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
