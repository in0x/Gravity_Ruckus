using UnityEngine;

public class ScreenDamageEffect : MonoBehaviour
{
    public float m_fDurationSeconds = 0.5f;

    Material m_material;
    Camera m_camera;
    int m_pixelWidth;
    int m_pixelHeight;
    float m_fTimeSinceActivate;
    float m_fIntensity;
    bool m_bActiveDmg = false;

    bool m_bDrawBlack = false;
    CameraClearFlags defaultClear;
    int defaultCullmask;

    void Start()
    {
    }

    public void Activate(float _intensity)
    {
        m_bActiveDmg = true;
        if (_intensity < 0) _intensity = 0;
        m_fIntensity = _intensity;
    }

    public void ToggleBlack()
    {
        m_bDrawBlack = !m_bDrawBlack;

        if (m_bDrawBlack)
        {
            m_camera.clearFlags = CameraClearFlags.SolidColor;
            m_camera.cullingMask = 0;
        }
        else
        {
            m_camera.clearFlags = defaultClear;
            m_camera.cullingMask = defaultCullmask;
        }  
    }

    // Creates a private material used to the effect
    void Awake()
    {
        m_camera = GetComponent<Camera>();
        m_pixelWidth = m_camera.pixelWidth;
        m_pixelHeight = m_camera.pixelHeight;

        defaultClear = m_camera.clearFlags;
        defaultCullmask = m_camera.cullingMask;
        m_camera.backgroundColor = Color.black;

        m_material = new Material(Shader.Find("Screen/Damage"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!m_bActiveDmg) return;

        m_fTimeSinceActivate += Time.deltaTime;

        if (m_fTimeSinceActivate >= m_fDurationSeconds)
        {
            m_fTimeSinceActivate = 0f;
            m_bActiveDmg = false;
        }

        if (m_fIntensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        m_material.SetFloat("_Intensity", m_fIntensity);
        m_material.SetInt("_ScreenWidth", m_pixelWidth);
        m_material.SetInt("_ScreenHeight", m_pixelHeight);
        Graphics.Blit(source, destination, m_material);
    }
}

    