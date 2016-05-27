using UnityEngine;

[ExecuteInEditMode]
public class ScreenDamageEffect : MonoBehaviour
{
    public float m_fDurationSeconds = 0.5f;

    private Material material;

    int m_pixelWidth;
    int m_pixelHeight;

    float m_fTimeSinceActivate;
    float m_fIntensity;

    bool m_bActive = false;

    void Start()
    {
        Camera camera = GetComponent<Camera>();
        m_pixelWidth = camera.pixelWidth;
        m_pixelHeight = camera.pixelHeight;
    }

    public void Activate(float _intensity)
    {
        m_bActive = true;
        if (_intensity < 0) _intensity = 0;
        m_fIntensity = _intensity;
    }

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Screen/Damage"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!m_bActive) return;

        m_fTimeSinceActivate += Time.deltaTime;

        if (m_fTimeSinceActivate >= m_fDurationSeconds)
        {
            m_fTimeSinceActivate = 0f;
            m_bActive = false;
        }

        if (m_fIntensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_Intensity", m_fIntensity);
        material.SetInt("_ScreenWidth", m_pixelWidth);
        material.SetInt("_ScreenHeight", m_pixelHeight);
        Graphics.Blit(source, destination, material);
    }
}

    