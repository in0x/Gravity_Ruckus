using UnityEngine;

public class TorsoDamageReciever : MonoBehaviour, IDamageReciever
{
    public HealthController HealthController { get; set; }

    public void RecieveDamage(DamageInfo damageInfo)
    {
        damageInfo.fDamage *= 1f;
        HealthController.ApplyDamage(damageInfo);
    }
}
