using UnityEngine;
using System.Collections;

public class DrawCrosshair : MonoBehaviour
{
    public Texture2D texture;

    void OnGUI()
    {
        float xMin = (Screen.width / 2) - (texture.width / 8);
        float yMin = (Screen.height / 2) - (texture.height / 8);
        GUI.DrawTexture(new Rect(xMin, yMin, texture.width / 4, texture.height / 4), texture);
    }
}
