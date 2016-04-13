using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour, IInputObserver
{
    public PlayerInput PlayerInputRef { get; set; }
    public float m_fJumpHeight = 10f;
    public float m_xLookMul = 1f;
    public float m_yLookMul = 1f;

    Vector3 posInput;
    float xRotInput;
    float yRotInput;

    float speed = 150;
    Rigidbody m_RigidBody;
    Transform cam;

    void FixedUpdate()
    {
        Vector3 moveInput;
        Vector3 rotInput;
        bool jump = false;

        float h = PlayerInputRef.GetAxis("MoveHor");
        float v = PlayerInputRef.GetAxis("MoveVert");

        float x = PlayerInputRef.GetAxis("LookHor");
        float y = PlayerInputRef.GetAxis("LookVert"); ;

        // Jump currently has no cooldown.
        if (PlayerInputRef.GetAxis("Jump") != 0)
        {
            jump = true;
        }

        moveInput = new Vector3(h, 0, v);
        rotInput = new Vector3(x, y, 0);
        MoveInput(moveInput, rotInput, jump);
    }

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
