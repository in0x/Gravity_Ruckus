using UnityEngine;

/*\
|*| Should be implemented by all projectiles, used as  
|*| common base so that weapons can find a projectile
|*| component in the prefab and set themself as the source.
\*/
public interface IDamageSender
{
    GameObject SourceWeapon { get; set; }
}
