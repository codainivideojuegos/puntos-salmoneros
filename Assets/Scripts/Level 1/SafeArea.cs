using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    #region Variables

    [Header("Transformacion")]
    private RectTransform safeAreaRT = null; // Referencia al RectTransform del area segura.

    #endregion

    #region Metodos de Unity

    // Se llama al despertar el objeto.
    private void Awake()
    {
        Initialize();
    }

    #endregion

    #region Inicializacion

    // Inicializa el objeto.
    private void Initialize()
    {
        safeAreaRT = GetComponent<RectTransform>();

        ApplySafeArea();
    }

    #endregion

    #region Area Segura

    // Aplica el area segura.
    private void ApplySafeArea()
    {
        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        safeAreaRT.anchorMin = anchorMin;
        safeAreaRT.anchorMax = anchorMax;
    }

    #endregion
}