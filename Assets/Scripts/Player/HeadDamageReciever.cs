using UnityEngine;

public class HeadDamageReciever : MonoBehaviour, IDamageReciever
{
    public HealthController HealthController { get; set; }

    public void RecieveDamage(DamageInfo damageInfo)
    {
        damageInfo.bHeadshot = true;
        damageInfo.fDamage *= 2f;
        HealthController.ApplyDamage(damageInfo);
    }
}
