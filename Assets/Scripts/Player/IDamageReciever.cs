using UnityEngine;

/*\
|*| Any class that should recieve damage from any source
|*| should implement this interface. RecieveDamage() will
|*| be called by the weapons via sendMessage() to transmit 
|*| their initially calculated damage. 
\*/

public struct DamageInfo
{
    public DamageInfo(GameObject _sender, float damage, bool headshot = false, bool killedByPlayer = true)
    {
        if (killedByPlayer)
        {
            sender = _sender.transform.parent.gameObject;
        }
        else
        {
            sender = _sender;
        }

        // Player name.
        senderName = _sender.transform.parent.gameObject.name;
        // Weapon name.
        sourceName = _sender.name;

        fDamage = damage;
        bHeadshot = headshot;
    }

    public string senderName;
    public string sourceName;
    public bool bHeadshot;
    public float fDamage;
    public GameObject sender;
}

interface IDamageReciever
{
    // Establishes the connection between reciever and controller.
    // Will be set by the Player's HealthController on start-up.
    HealthController HealthController { get; set; }

    // Called via messageParsing by weapons to indicate that this 
    // Reciever has been hit
    void RecieveDamage(DamageInfo damageInfo);
}