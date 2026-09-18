 using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Test2 : MonoBehaviour
{
    public List<Preguntas> preguntas;
    public GameObject canva;
    public GameObject botonConfirmar;

    public Sprite spriteNormal;
    public Sprite spriteMarcado;

    public Conter counter;

    [Header("Obstaculos")]
    [SerializeField] private GeneradorNivel Coso;

    [Header("Textos")]
    public TMP_Text textopregunta;
    public TMP_Text textorespuesta1;
    public TMP_Text textorespuesta2;
    public TMP_Text textorespuesta3;
    public TMP_Text textorespuesta4;

    private Preguntas preguntaActual;
    private bool[] respuestasMarcadas = new bool[4];

    [Header("Botones")]
    public Button[] botonesRespuestas;

    [Header("Feedback")]
    [SerializeField] private float tiempoFeedback = 1f;

    private bool esperandoFeedback = false;


    void Start()
    {
        canva.SetActive(false);
    }


    public void NuevaPregunta()
    {
        if (preguntas.Count == 0)
        {
            Debug.Log("No quedan preguntas.");
            return;
        }

        esperandoFeedback = false;

        canva.SetActive(true);

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
            botonesRespuestas[i].image.color = Color.white;
            botonesRespuestas[i].interactable = true;
        }

        botonConfirmar.SetActive(
            preguntaActual.boolquedefinesiesESApregunta
        );

        preguntas.RemoveAt(indice);
    }


    public void Responder(int respuesta)
    {
        if (preguntaActual == null || esperandoFeedback)
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

        int indiceRespuesta = respuesta - 1;

        if (indiceRespuesta < 0 || indiceRespuesta >= botonesRespuestas.Length)
            return;

        bool correcto = respuesta == preguntaActual.repuetanume;

        if (correcto)
        {
            counter.puntu += 1;
        }

        StartCoroutine(MostrarResultadoBoton(correcto, indiceRespuesta));
    }

private IEnumerator MostrarResultadoBoton(bool correcto, int indiceBoton)
{
    esperandoFeedback = true;

    for (int i = 0; i < botonesRespuestas.Length; i++)
    {
        botonesRespuestas[i].interactable = false;
    }

    botonConfirmar.SetActive(false);

    botonesRespuestas[indiceBoton].image.color =
        correcto ? Color.green : Color.red;

    yield return new WaitForSeconds(tiempoFeedback);

    botonesRespuestas[indiceBoton].image.color = Color.white;

    canva.SetActive(false);

    preguntaActual = null;
    respuestasMarcadas = new bool[4];

    esperandoFeedback = false;

    EmpezarObstaculos();
}
    public void ConfirmarRespuesta()
    {
        if (preguntaActual == null || esperandoFeedback)
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
        counter.puntu += 1;
    }

    StartCoroutine(MostrarResultadoMultiple(correcto));
    }
    private IEnumerator MostrarResultadoMultiple(bool correcto)
{
    esperandoFeedback = true;

    botonConfirmar.SetActive(false);

    for (int i = 0; i < 4; i++)
    {
        if (i >= botonesRespuestas.Length)
            continue;

        botonesRespuestas[i].interactable = false;

        if (respuestasMarcadas[i])
        {
            bool respuestaCorrecta = ObtenerRespuestaCorrecta(i);

            botonesRespuestas[i].image.color =
                respuestaCorrecta ? Color.green : Color.red;
        }
        //gracias oscar
    }

    yield return new WaitForSeconds(tiempoFeedback);

    for (int i = 0; i < botonesRespuestas.Length; i++)
    {
        botonesRespuestas[i].image.color = Color.white;
        botonesRespuestas[i].image.sprite = spriteNormal;
        botonesRespuestas[i].interactable = true;
    }

    canva.SetActive(false);

    preguntaActual = null;
    respuestasMarcadas = new bool[4];

    esperandoFeedback = false;

    EmpezarObstaculos();
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


    private void EmpezarObstaculos()
    {
        Coso.Empezar();
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