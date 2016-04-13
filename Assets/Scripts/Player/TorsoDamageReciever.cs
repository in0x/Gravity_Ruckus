using UnityEngine;
using System.Collections;

public class TorsoDamageReciever : MonoBehaviour, IDamageReciever
{

    public HealthController HealthController { get; set; }

    public void RecieveDamage(float dmg)
    {
        Debug.Log("Torso dmg");
        HealthController.ApplyDamage(dmg * 1f);
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
