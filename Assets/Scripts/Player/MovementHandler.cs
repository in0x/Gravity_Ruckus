using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour
{
    public float m_fJumpHeight = 10f;
    public float m_xLookMul = 1f;
    public float m_yLookMul = 1f;

    Vector3 posInput;
    float xRotInput;
    float yRotInput;

    float speed = 150;
    Rigidbody m_RigidBody;
    Transform cam;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        // Find the transform of the parents camera component
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "MainCamera") cam = child;
        }
    }

    public void MoveInput(Vector3 move, Vector3 rote, bool jump = false)
    {
        posInput = move;
        xRotInput = rote.x;
        yRotInput = rote.y;

        if (jump)
        {
            posInput.y += m_fJumpHeight;
        }

        ActualMove();
    }

    void ActualMove()
    {
        m_RigidBody.AddRelativeForce(posInput * speed);

        transform.localRotation *= Quaternion.Euler(0, xRotInput * m_xLookMul, 0);
        Quaternion quat = Quaternion.Euler(yRotInput * m_yLookMul, 0, 0);

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
