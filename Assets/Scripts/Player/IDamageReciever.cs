using UnityEngine;

/*\
|*| Any class that should recieve damage from any source
|*| should implement this interface. RecieveDamage() will
|*| be called by the weapons via sendMessage() to transmit 
|*| their initially calculated damage. 
\*/

public struct DamageInfo
{
    public DamageInfo(GameObject sender, float damage, bool headshot = false)
    {
        // Player name.
        senderName = sender.transform.parent.gameObject.name;
        // Weapon name.
        sourceName = sender.name;

        fDamage = damage;
        bHeadshot = headshot;
    }

    public string senderName;
    public string sourceName;
    public bool bHeadshot;
    public float fDamage;
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