using UnityEngine;
using System.Collections;

/*\
|*| Should be implemented by all projectiles, used as  
|*| common base so that weapons can find a projectile
|*| component in the prefab and set themself as the source.
\*/
public interface IDamageSender
{
    // Used for information that is send to damage reciever 
    // about the sender and the weapon used.
    GameObject SourceWeapon { get; set; }
}
