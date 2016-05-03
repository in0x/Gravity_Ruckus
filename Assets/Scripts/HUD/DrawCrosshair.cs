using UnityEngine;
using System.Collections;

public class DrawCrosshair : MonoBehaviour
{
    public Texture2D texture;

    private float xMin;
    private float yMin;

    void Start()
    {
        Camera parentCamera = GetComponentInParent<Camera>();
        Rect cameraRect = parentCamera.pixelRect;
        xMin = cameraRect.xMin + (cameraRect.width / 2) - (texture.width / 8f);
        yMin = cameraRect.yMin + (cameraRect.height / 2) - (texture.height / 8f);
    }

    void OnGUI()
    {
        
        GUI.DrawTexture(new Rect(xMin, yMin, texture.width / 4f, texture.height / 4f), texture);
    }
}
