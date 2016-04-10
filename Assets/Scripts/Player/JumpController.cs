using UnityEngine;
using System.Collections;

public class JumpController : MonoBehaviour
{
    bool m_isJumping = false;
   
    public bool isJumping
    {
        get
        {   if (!m_isJumping)
            {
                m_isJumping = true;
                return false;
            }

            return true;
        }

        private set { m_isJumping = value; }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "LevelGeometry") m_isJumping = false;
    }
}
