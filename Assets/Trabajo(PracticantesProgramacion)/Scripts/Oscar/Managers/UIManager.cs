using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Vida")]
    [SerializeField] private GameObject prefabVida;
    [SerializeField] private Transform corazones;

    [Header("Fade")]
    [SerializeField] private Image pantallaFade;
    [SerializeField] private float duracionFade;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Inicializar();
    }

    private void Inicializar()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(prefabVida, corazones);
        }
    }

    public void ActualizarVida(int vida)
    {
        if (corazones.childCount > vida)
        {
            Destroy(corazones.GetChild(corazones.childCount - 1).gameObject);
        }
    }

    public IEnumerator HaciendoFade(string DedondeViene = "")
    {
        if (DedondeViene == "Player")
            yield return new WaitForSeconds(2f);
            
        yield return Fade();

        SceneManager.LoadScene("Menu");
    }

    private IEnumerator Fade()
    {
        Color colorInicial = pantallaFade.color;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionFade)
        {
            tiempoTranscurrido += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, tiempoTranscurrido / duracionFade);
            pantallaFade.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, alpha);
            yield return null;
        }

        pantallaFade.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 1f);
    }
}
