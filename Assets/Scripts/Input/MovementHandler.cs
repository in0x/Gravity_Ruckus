using UnityEngine;

public class MovementHandler : MonoBehaviour, IInputObserver
{
    public PlayerInput m_playerInputRef { get; set; }
    public GameObject m_camera;
    public float m_fJumpHeight = 10f;
    public float m_xLookMul = 1f;
    public float m_yLookMul = 1f;
    public float m_moveSpeed = 250;

    Vector3 m_posInput;
    float m_xRotInput;
    float m_yRotInput;

    Rigidbody m_RigidBody;
    JumpController m_jumpController;
    Transform m_cam;
    AimAssist m_aimAssist;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_jumpController = GetComponent<JumpController>();
        m_aimAssist = GetComponent<AimAssist>();

        m_cam = m_camera.transform;

        //foreach (Transform child in gameObject.transform)
        //{
        //    if (child.tag == "MainCamera") m_cam = child;
        //}
    }

    void FixedUpdate()
    {
        Vector3 moveInput;
        Vector3 rotInput;
        bool jump = false;

        float h = m_playerInputRef.GetAxis("MoveHor");
        float v = m_playerInputRef.GetAxis("MoveVert");

        float x = m_playerInputRef.GetAxis("LookHor");
        float y = -m_playerInputRef.GetAxis("LookVert"); 
        
        if (m_playerInputRef.GetAxis("Jump") != 0)
        {
            jump = true;
        }

        moveInput = new Vector3(h, 0, v);
        rotInput = new Vector3(x, y, 0);
        MoveInput(moveInput, rotInput, jump);
    }
    
    public void MoveInput(Vector3 move, Vector3 rot, bool wantsToJump = false)
    {
        m_posInput = move;
        m_xRotInput = rot.x;
        m_yRotInput = rot.y;

        if (wantsToJump)
        {
            if (m_jumpController.CanJump())
            {
                m_posInput.y += m_fJumpHeight;
            }
        }

        ActualMove();
    }

    void ActualMove()
    {
        m_RigidBody.AddRelativeForce(m_posInput * m_moveSpeed);

        transform.localRotation *= Quaternion.Euler(0, m_xRotInput * m_xLookMul, 0);
        Quaternion quat = Quaternion.Euler(m_yRotInput * m_yLookMul, 0, 0);

        if (m_cam.localRotation.eulerAngles.x > 80 && m_cam.localRotation.eulerAngles.x < 180 && quat.x < 0) 
        {
            m_cam.localRotation *= quat;
        }
        else if (m_cam.localRotation.eulerAngles.x < 275 && m_cam.localRotation.eulerAngles.x > 180 && quat.x > 0) 
        {
            m_cam.localRotation *= quat;
        }
        else if (m_cam.localRotation.eulerAngles.x < 80 || m_cam.localRotation.eulerAngles.x > 275) 
        {
            m_cam.localRotation *= quat;
        }

        m_aimAssist.Adjust();
    }

}
