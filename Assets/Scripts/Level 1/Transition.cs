using System.Collections;
using UnityEngine;

// Clase principal que representa la transición.
public class Transition : MonoBehaviour
{
    [Header("Transición")]
    [HideInInspector] public bool isTransitioning;    // Si está transicionando o no.

    [SerializeField] private GameObject bgImageGO;    // Objeto de la imagen de fondo.
    [SerializeField] private RectTransform maskImage; // Transformación recta de la imagen de máscara.
    [SerializeField] private float waitTime;          // Tiempo de espera.
    [SerializeField] private float duration;          // Duración.

    private Coroutine scaleInCoroutine = null;        // Corrutina de la entrada de la escala.
    private Coroutine scaleOutCoroutine = null;       // Corrutina de la salida de la escala.

    // MÉTODOS DE UNITY
    private void Start()
    {
        bgImageGO.SetActive(true);
        maskImage.localScale = Vector3.zero;

        StartScaleIn();
    }

    // MÉTODOS DE LA TRANSICIÓN

    // Método de iniciar la entrada de la escala.
    public void StartScaleIn()
    {
        if (scaleInCoroutine != null)
        {
            return;
        }

        scaleInCoroutine = StartCoroutine(ScaleIn());
    }

    // Método de iniciar la entrada de la salida.
    public void StartScaleOut()
    {
        if (scaleOutCoroutine != null)
        {
            return;
        }

        scaleOutCoroutine = StartCoroutine(ScaleOut());
    }

    // Corrutina de entrada de la escala.
    private IEnumerator ScaleIn()
    {
        isTransitioning = true;

        var startScale = Vector3.zero;
        var targetScale = Vector3.one;
        float elapsedTime = 0f;

        yield return new WaitForSeconds(waitTime);

        bgImageGO.SetActive(false);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;

            maskImage.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        maskImage.localScale = targetScale;
        scaleInCoroutine = null;
        isTransitioning = false;
    }

    // Corrutina de salida de la escala.
    private IEnumerator ScaleOut()
    {
        isTransitioning = true;

        var startScale = Vector3.one;
        var targetScale = Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;

            maskImage.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        maskImage.localScale = targetScale;

        yield return new WaitForSeconds(waitTime);

        bgImageGO.SetActive(true);
        scaleOutCoroutine = null;
        isTransitioning = false;
    }
}