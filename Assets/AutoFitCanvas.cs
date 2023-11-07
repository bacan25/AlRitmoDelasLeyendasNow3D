using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class AutoFitCanvas : MonoBehaviour
{
    public string screenPositionReferenceTag = "ScreenPositionReference";
    public string cameraPositionReferenceTag = "CameraPositionReference";
    public float zOffset = 0.1f; // Un pequeño desplazamiento en Z para evitar z-fighting

    private Canvas canvas;
    private RectTransform rectTransform;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvas.renderMode = RenderMode.WorldSpace;
    }

    void Start()
    {
        AdjustCanvas();
    }

    void AdjustCanvas()
    {
        GameObject screenPositionRef = GameObject.FindGameObjectWithTag(screenPositionReferenceTag);
        GameObject cameraPositionRef = GameObject.FindGameObjectWithTag(cameraPositionReferenceTag);

        if (screenPositionRef == null || cameraPositionRef == null)
        {
            Debug.LogError("Referencia de objeto no encontrada. Asegúrese de que los tags estén configurados correctamente.");
            return;
        }

        Camera renderCamera = cameraPositionRef.GetComponent<Camera>();
        if (renderCamera != null)
        {
            canvas.worldCamera = renderCamera;
        }
        else
        {
            Debug.LogError("No se encontró la cámara de referencia.");
            return;
        }

        // Ajustar la escala del canvas basándose en el tamaño de la RenderTexture
        if (renderCamera.targetTexture != null)
        {
            float scaleFactor = screenPositionRef.transform.localScale.x / renderCamera.targetTexture.width;
            rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }
        else
        {
            Debug.LogError("La cámara de referencia no tiene una RenderTexture asignada.");
        }

        // Ajustar la posición del canvas para que coincida con la del objeto de referencia de la pantalla
        // y aplicar el desplazamiento en Z.
        rectTransform.position = screenPositionRef.transform.position + new Vector3(0, 0, zOffset);

        // Ajustar la rotación para mirar hacia la cámara
        Vector3 directionToCamera = cameraPositionRef.transform.position - rectTransform.position;
        rectTransform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}