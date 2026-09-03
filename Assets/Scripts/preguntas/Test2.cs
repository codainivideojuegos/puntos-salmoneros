using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Test2 : MonoBehaviour
{
    public List<Preguntas> preguntas;
    public GameObject canva;
    public GameObject botonConfirmar;
    public Sprite spriteNormal;
    public Sprite spriteMarcado;
    public Conter counter;

    [Header("Textos")]
    public TMP_Text textopregunta;
    public TMP_Text textorespuesta1;
    public TMP_Text textorespuesta2;
    public TMP_Text textorespuesta3;
    public TMP_Text textorespuesta4;

    private Preguntas preguntaActual;
    private bool[] respuestasMarcadas = new bool[4];
    [Header("Botones")]
    public UnityEngine.UI.Button[] botonesRespuestas;

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

        respuestasMarcadas = new bool[4];

        for (int i = 0; i < botonesRespuestas.Length; i++)
        {
            botonesRespuestas[i].image.sprite = spriteNormal;
        }

        botonConfirmar.SetActive(preguntaActual.boolquedefinesiesESApregunta);

        preguntas.RemoveAt(indice);
    }

    public void Responder(int respuesta)
    {
        if (preguntaActual == null)
            return;

        if (preguntaActual.boolquedefinesiesESApregunta)
        {
            int indice = respuesta - 1;

            if (indice < 0 || indice >= 4)
                return;

            if (indice >= botonesRespuestas.Length)
                return;

            respuestasMarcadas[indice] = !respuestasMarcadas[indice];

            if (respuestasMarcadas[indice])
            {
                botonesRespuestas[indice].image.sprite = spriteMarcado;
            }
            else
            {
                botonesRespuestas[indice].image.sprite = spriteNormal;
            }

            return;
        }

        ComprobarRespuesta(respuesta);
    }

    public void ConfirmarRespuesta()
    {
        if (preguntaActual == null)
            return;

        if (!preguntaActual.boolquedefinesiesESApregunta)
            return;

        bool correcto = true;

        for (int i = 0; i < 4; i++)
        {
            bool correcta = ObtenerRespuestaCorrecta(i);
            bool marcada = respuestasMarcadas[i];

            if (correcta != marcada)
            {
                correcto = false;
                break;
            }
        }

        if (correcto)
        {
            Debug.Log("yipi");
            counter.puntu += 1;
        }
        else
        {
            Debug.Log("yopo");
        }

        canva.SetActive(false);
        preguntaActual = null;
        respuestasMarcadas = new bool[4];
    }

    void ComprobarRespuesta(int respuesta)
    {
        if (respuesta == preguntaActual.repuetanume)
        {
            Debug.Log("yipi");
            counter.puntu += 1;
        }
        else
        {
            Debug.Log("yopo");
        }

        canva.SetActive(false);
        preguntaActual = null;
    }

    bool ObtenerRespuestaCorrecta(int indice)
    {
        switch (indice)
        {
            case 0:
                return preguntaActual.respuesta1Correcta;

            case 1:
                return preguntaActual.respuesta2Correcta;

            case 2:
                return preguntaActual.respuesta3Correcta;

            case 3:
                return preguntaActual.respuesta4Correcta;

            default:
                return false;
        }
    }
}

[System.Serializable]
public class Preguntas
{
    [Header("Pregunta")]
    [TextArea(1, 3)]
    public string pregunta;

    [Header("Respuestas")]
    [TextArea(1, 3)]
    public string respuesta1;

    [TextArea(1, 3)]
    public string respuesta2;

    [TextArea(1, 3)]
    public string respuesta3;

    [TextArea(1, 3)]
    public string respuesta4;

    [Header("Configuración")]
    public bool boolquedefinesiesESApregunta;

    [Header("Respuestas correctas")]
    public bool respuesta1Correcta;
    public bool respuesta2Correcta;
    public bool respuesta3Correcta;
    public bool respuesta4Correcta;

    public int repuetanume;
}