using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Test2 : MonoBehaviour
{
    public List<Preguntas> preguntas;
    public GameObject canva;

    public TMP_Text textopregunta;
    public TMP_Text textorespuesta1;
    public TMP_Text textorespuesta2;
    public TMP_Text textorespuesta3;
    public TMP_Text textorespuesta4;
    private Preguntas preguntaActual;
    void Start()
    {
        canva.SetActive(false);
    }
    public void NuevaPregunta()
    {
        canva.SetActive(true);
        if (preguntas.Count == 0)
        {
            Debug.Log("hola!");
            return;
        }

        int indice = Random.Range(0, preguntas.Count);

        preguntaActual = preguntas[indice];

        textopregunta.text = preguntaActual.pregunta;
        textorespuesta1.text = preguntaActual.respuesta1;
        textorespuesta2.text = preguntaActual.respuesta2;
        textorespuesta3.text = preguntaActual.respuesta3;
        textorespuesta4.text = preguntaActual.respuesta4;

        preguntas.RemoveAt(indice);
    }
    public void Responder(int respuesta)
    {
        if (preguntaActual == null)
            return;

        if (respuesta == preguntaActual.repuetanume)
        {
            Debug.Log("yipi");
            canva.SetActive(false);
        }
        else
        {
            Debug.Log("yopo");
            canva.SetActive(false);
        }
    }
}

[System.Serializable]
public class Preguntas
{
    public string pregunta;
    public string respuesta1;
    public string respuesta2;
    public string respuesta3;
    public string respuesta4;
    public int repuetanume;
}