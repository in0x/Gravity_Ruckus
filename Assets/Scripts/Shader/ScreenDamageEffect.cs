using UnityEngine;

[ExecuteInEditMode]
public class ScreenDamageEffect : MonoBehaviour
{
    public float durationSeconds = 0.5f;

    private Material material;

    int pixelWidth;
    int pixelHeight;

    float timeSinceActivate;
    float intensity;

    bool active = false;

    void Start()
    {
        Camera camera = GetComponent<Camera>();
        pixelWidth = camera.pixelWidth;
        pixelHeight = camera.pixelHeight;
    }

    public void Activate(float _intensity)
    {
        active = true;

        if (_intensity < 0) _intensity = 0;

        intensity = _intensity;
    }

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Screen/Damage"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!active) return;

        timeSinceActivate += Time.deltaTime;

        if (timeSinceActivate >= durationSeconds)
        {
            timeSinceActivate = 0f;
            active = false;
        }

        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_Intensity", intensity);
        material.SetInt("_ScreenWidth", pixelWidth);
        material.SetInt("_ScreenHeight", pixelHeight);
        Graphics.Blit(source, destination, material);
    }
}

    