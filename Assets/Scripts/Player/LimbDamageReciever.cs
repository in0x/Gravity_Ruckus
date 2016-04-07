using UnityEngine;
using System.Collections;

public class LimbDamageReciever : MonoBehaviour, IDamageReciever
{
    public HealthController HealthController { get; set; }

    public void RecieveDamage(float dmg)
    {
        Debug.Log("Limb Dmg");
        HealthController.ApplyDamage(dmg * 0.5f);
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
