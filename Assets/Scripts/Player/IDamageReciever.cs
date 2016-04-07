/*\
|*| Any class that should recieve damage from any source
|*| should implement this interface. RecieveDamage() will
|*| be called by the weapons via sendMessage() to transmit 
|*| their initially calculated damage. 
\*/

interface IDamageReciever
{
    // Establishes the connection between reciever and controller.
    // Will be set by the Player's HealthController on start-up.
    HealthController HealthController { get; set; }

    // Called via messageParsing by weapons to indicate that this 
    // Reciever has been hit
    void RecieveDamage(float dmg);
}