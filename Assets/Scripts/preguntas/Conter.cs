using TMPro;
using UnityEngine;
using System.Collections;

public class Conter : MonoBehaviour
{
    [Header("Puntaje")]
    public PuntajeData puntajeData;

    [Header("UI")]
    public TMP_Text textonumerorespuestas;
    public TMP_Text textonumerorespuestas2;
    public GameObject canvabueno;

    [Header("Configuración")]
    [SerializeField] private float tiempoVisible = 10f;


    // ==========================================
    // CICLO DE VIDA
    // ==========================================

    void Start()
    {
        canvabueno.SetActive(false);
    }


    // ==========================================
    // PUNTAJE
    // ==========================================

    public void SumarPunto()
    {
        puntajeData.Sumar();
    }


    // ==========================================
    // PANEL DE RESULTADO FINAL
    // ==========================================

    public void MostrarResultadoFinal(int totalPreguntas)
    {
        StartCoroutine(Check(totalPreguntas));
    }

    private IEnumerator Check(int totalPreguntas)
    {
        canvabueno.SetActive(true);

        textonumerorespuestas.text = "Respondiste " + puntajeData.Puntaje + " preguntas de " + totalPreguntas + "!";

        textonumerorespuestas2.text = (puntajeData.Puntaje == totalPreguntas)
            ? "Felicidades!"
            : "Buen intento, sigue practicando!";

        yield return new WaitForSeconds(tiempoVisible);

        canvabueno.SetActive(false);
    }
}