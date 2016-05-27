using UnityEngine;

public class LimbDamageReciever : MonoBehaviour, IDamageReciever
{
    public HealthController HealthController { get; set; }

    public void RecieveDamage(DamageInfo damageInfo)
    {
        damageInfo.fDamage *= 0.5f;
        HealthController.ApplyDamage(damageInfo);
    }
}
