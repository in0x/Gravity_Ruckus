using UnityEngine;
using System.Collections;

public class LimbDamageReciever : MonoBehaviour, IDamageReciever
{
    public HealthController HealthController { get; set; }

    public void RecieveDamage(DamageInfo damageInfo)
    {
        Debug.Log("Limb Dmg");
        damageInfo.fDamage *= 0.5f;
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
