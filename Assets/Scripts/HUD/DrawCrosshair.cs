using UnityEngine;

public class DrawCrosshair : MonoBehaviour
{
    public Texture2D m_texture;

    private float m_xMin;
    private float m_yMin;

    void Start()
    {
        Camera parentCamera = GetComponentInParent<Camera>();
        Rect cameraRect = parentCamera.pixelRect;
        m_xMin = cameraRect.xMin + (cameraRect.width / 2) - (m_texture.width / 8f);
        m_yMin = cameraRect.yMin + (cameraRect.height / 2) - (m_texture.height / 8f);
    }

    void OnGUI()
    {   
        GUI.DrawTexture(new Rect(m_xMin, m_yMin, m_texture.width / 4f, m_texture.height / 4f), m_texture);
    }
}
