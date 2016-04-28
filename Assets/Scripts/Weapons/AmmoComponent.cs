using UnityEngine;

public class AmmoComponent : MonoBehaviour
{
    // Maximum capacity.
    public int m_maxAmmo;

    // Currently availible capacity.
    int m_currentAmmo;
    
    // Replenish ammo completely.
    public void RefillComplete()
    {
        m_currentAmmo = m_maxAmmo;
    }

    // Refill count units, up to max.
    public void Refill(int count)
    {
        m_currentAmmo += count;
        m_currentAmmo = Mathf.Min(m_currentAmmo, m_maxAmmo);
    }
    
    // Internal. Tries to use a single ammo unit. True on success (ammo was availible).
    bool use()
    {
        if (m_currentAmmo > 0)
        {
            m_currentAmmo--;
            return true;
        }
        else return false;
    }

    // Public. Pass how many units you want to use. Returns how many units were actually availble.
    // If becomes empty, the used amount up to that point is returned.
    public int UseAmmo(int count)
    {
        int usedAmmo = 0;

        for (int i = 0; i < count; i++)
        {
            if (use())
            {
                usedAmmo++;
            }
            else break;
        }
             
        return usedAmmo;
    }
}
