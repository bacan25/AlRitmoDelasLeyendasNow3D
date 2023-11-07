using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedSizeRenderTexture : MonoBehaviour
{
    public int textureWidth = 1920;
    public int textureHeight = 1080;

    private Camera _camera;
    private RenderTexture _renderTexture;

    void Start()
    {
        _camera = GetComponent<Camera>();
        UpdateRenderTexture();
    }
    public void UpdateRenderTextureBasedOnScreenSize()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        int textureHeight = Mathf.RoundToInt(textureWidth / screenAspect);

        SetTextureSize(textureWidth, textureHeight);
    }

    void UpdateRenderTexture()
    {
        if (_renderTexture != null)
        {
            _renderTexture.Release();
        }

        _renderTexture = new RenderTexture(textureWidth, textureHeight, 24);
        _camera.targetTexture = _renderTexture;
    }

    // Llamar a esta función si necesitas actualizar el tamaño en tiempo de ejecución
    public void SetTextureSize(int width, int height)
    {
        textureWidth = width;
        textureHeight = height;
        UpdateRenderTexture();
    }

    void OnDestroy()
    {
        if (_renderTexture != null)
        {
            _renderTexture.Release();
        }
    }
}
