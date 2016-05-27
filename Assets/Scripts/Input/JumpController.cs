using UnityEngine;

public class JumpController : MonoBehaviour
{
    bool m_isJumping = false;
   
    public bool IsJumping
    {
        get { return m_isJumping; }
        set { m_isJumping = value; }
    }

    public bool CanJump()
    {
        if (!m_isJumping)
        {
            m_isJumping = true;
            return true;
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (m_isJumping)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.thisCollider.CompareTag("LegCollider")) m_isJumping = false;               
            }
        }
    }
}
