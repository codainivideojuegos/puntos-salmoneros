using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    [Header("Ganar y Perder")]
    [SerializeField] private GameObject Panel_Ganar;
    [SerializeField] private GameObject Panel_Perder;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void EsRespuestaCorrecta(int opcionElegida, Button boton)
    {
        bool esCorrecta = opcionElegida == PreguntasManager.Instance.RespuestaCorrecta;

        boton.image.color = esCorrecta ? Color.green : Color.red;

        AudioManagerOscar.Instance.changeSFX(esCorrecta ? "Opcion Correcta" : "Opcion Incorrecta");

        StartCoroutine(EsperarAntesActivarPanel(esCorrecta));
    }


    public void Menu()
    {
        StartCoroutine(UIManagerOscar.Instance.HaciendoFade());
    }

    private IEnumerator EsperarAntesActivarPanel(bool esCorrecta)
    {
        yield return new WaitForSeconds(1f);

        if (esCorrecta)
        {
            Panel_Ganar.SetActive(true);
        }
        else
        {
            Panel_Perder.SetActive(true);
        }
    }
}
