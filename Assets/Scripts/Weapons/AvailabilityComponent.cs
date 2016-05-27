using UnityEngine;

public class AvailabilityComponent : MonoBehaviour
{
    bool m_outOfAmmo;
    bool m_available;

    public bool OutOfAmmo
    {
        get { return m_outOfAmmo; } set { m_outOfAmmo = value; }
    }

    public bool IsAvailable
    {
        get { return m_available; } set { m_available = value; }
    }


}
