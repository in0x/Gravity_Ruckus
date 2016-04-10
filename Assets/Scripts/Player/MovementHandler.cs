using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour
{
    public float fJumpHeight = 10f;
    public float xLookMul = 1f;
    public float yLookMul = 1f;
    public float moveSpeed = 150;

    Vector3 m_posInput;
    float m_xRotInput;
    float m_yRotInput;

    Rigidbody m_RigidBody;
    JumpController jumpController;
    Transform cam;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        jumpController = GetComponent<JumpController>();

        // Find the transform of the parents camera component
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "MainCamera") cam = child;
        }
    }
    
    public void MoveInput(Vector3 move, Vector3 rote, bool wantsToJump = false)
    {
        m_posInput = move;
        m_xRotInput = rote.x;
        m_yRotInput = rote.y;

        if (wantsToJump)
        {
            if (jumpController.canJump())
            {
                m_posInput.y += fJumpHeight;
            }
        }

        ActualMove();
    }

    void ActualMove()
    {
        m_RigidBody.AddRelativeForce(m_posInput * moveSpeed);

        transform.localRotation *= Quaternion.Euler(0, m_xRotInput * xLookMul, 0);
        Quaternion quat = Quaternion.Euler(m_yRotInput * yLookMul, 0, 0);

        if (cam.localRotation.eulerAngles.x > 80 && cam.localRotation.eulerAngles.x < 180 && quat.x < 0) // > 60 && < 180 && < 0
        {
            cam.localRotation *= quat;
        }
        else if (cam.localRotation.eulerAngles.x < 275 && cam.localRotation.eulerAngles.x > 180 && quat.x > 0) // < 300 && > 180 && > 0
        {
            cam.localRotation *= quat;
        }
        else if (cam.localRotation.eulerAngles.x < 80 || cam.localRotation.eulerAngles.x > 275) // < 60 || > 300
        {
            cam.localRotation *= quat;
        }

    }

}
