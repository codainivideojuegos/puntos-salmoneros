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

    private int indicePreguntaActual = -1;


    void Start()
    {
        canva.SetActive(false);

        if (counter != null)
        {
            counter.puntu = Sumba.buenas;
        }
    }


    public void NuevaPregunta()
    {
        if (esperandoFeedback)
            return;

        List<int> preguntasDisponibles = new List<int>();

        for (int i = 0; i < preguntas.Count; i++)
        {
            if (!Sumba.EstaHecha(i))
            {
                preguntasDisponibles.Add(i);
            }
        }

        if (preguntasDisponibles.Count == 0)
        {
            Debug.Log("No quedan preguntas nuevas.");

            canva.SetActive(false);

            return;
        }

        esperandoFeedback = false;

        canva.SetActive(true);

        int posicionAleatoria = Random.Range(0, preguntasDisponibles.Count);

        indicePreguntaActual = preguntasDisponibles[posicionAleatoria];

        preguntaActual = preguntas[indicePreguntaActual];

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

        Sumba.MarcarPregunta(indicePreguntaActual);
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

            respuestasMarcadas[indice] =
                !respuestasMarcadas[indice];

            if (respuestasMarcadas[indice])
            {
                botonesRespuestas[indice].image.sprite =
                    spriteMarcado;
            }
            else
            {
                botonesRespuestas[indice].image.sprite =
                    spriteNormal;
            }

            return;
        }

        int indiceRespuesta = respuesta - 1;

        if (indiceRespuesta < 0 ||
            indiceRespuesta >= botonesRespuestas.Length)
            return;

        bool correcto =
            respuesta == preguntaActual.repuetanume;


        Sumba.Guardar(correcto);

        if (counter != null)
        {
            counter.puntu = Sumba.buenas;
        }

        StartCoroutine(
            MostrarResultadoBoton(
                correcto,
                indiceRespuesta
            )
        );
    }


    private IEnumerator MostrarResultadoBoton(
        bool correcto,
        int indiceBoton)
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

        botonesRespuestas[indiceBoton].image.color =
            Color.white;

        canva.SetActive(false);

        preguntaActual = null;

        indicePreguntaActual = -1;

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
            bool correcta =
                ObtenerRespuestaCorrecta(i);

            bool marcada =
                respuestasMarcadas[i];


            if (correcta != marcada)
            {
                correcto = false;
                break;
            }
        }


        Sumba.Guardar(correcto);


        if (counter != null)
        {
            counter.puntu =
                Sumba.buenas;
        }


        StartCoroutine(
            MostrarResultadoMultiple(correcto)
        );
    }


    private IEnumerator MostrarResultadoMultiple(
        bool correcto)
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
                bool respuestaCorrecta =
                    ObtenerRespuestaCorrecta(i);


                botonesRespuestas[i].image.color =
                    respuestaCorrecta
                    ? Color.green
                    : Color.red;
            }
        }


        yield return new WaitForSeconds(
            tiempoFeedback
        );


        for (int i = 0; i < botonesRespuestas.Length; i++)
        {
            botonesRespuestas[i].image.color =
                Color.white;

            botonesRespuestas[i].image.sprite =
                spriteNormal;

            botonesRespuestas[i].interactable =
                true;
        }


        canva.SetActive(false);

        preguntaActual = null;

        indicePreguntaActual = -1;

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
        if (Coso != null)
        {
            Coso.Empezar();
        }
    }
    public void Yatusabeyatusabe()
    {
        Sumba.Matar();
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



public static class Sumba
{

    public static bool pregunta1;
    public static bool pregunta2;
    public static bool pregunta3;
    public static bool pregunta4;
    public static bool pregunta5;
    public static bool pregunta6;
    public static bool pregunta7;
    public static bool pregunta8;
    public static bool pregunta9;
    public static bool pregunta10;


    public static int buenas;
    public static int hechas;
    public static int buenasCheckpoint;
    public static int hechasCheckpoint;

    public static void GuardarCheckpoint()
    {
        buenasCheckpoint = buenas;
        hechasCheckpoint = hechas;
    }

    public static void ReiniciarCheckpoint()
    {
        buenas = buenasCheckpoint;
        hechas = hechasCheckpoint;
    }
    public static void Guardar(bool correcta)
    {
        hechas++;

        if (correcta)
        {
            buenas++;
        }
    }

    public static void MarcarPregunta(int indice)
    {
        switch (indice)
        {
            case 0:
                pregunta1 = true;
                break;

            case 1:
                pregunta2 = true;
                break;

            case 2:
                pregunta3 = true;
                break;

            case 3:
                pregunta4 = true;
                break;

            case 4:
                pregunta5 = true;
                break;

            case 5:
                pregunta6 = true;
                break;

            case 6:
                pregunta7 = true;
                break;

            case 7:
                pregunta8 = true;
                break;

            case 8:
                pregunta9 = true;
                break;

            case 9:
                pregunta10 = true;
                break;
        }
    }

    public static bool EstaHecha(int indice)
    {
        switch (indice)
        {
            case 0:
                return pregunta1;

            case 1:
                return pregunta2;

            case 2:
                return pregunta3;

            case 3:
                return pregunta4;

            case 4:
                return pregunta5;

            case 5:
                return pregunta6;

            case 6:
                return pregunta7;

            case 7:
                return pregunta8;

            case 8:
                return pregunta9;

            case 9:
                return pregunta10;

            default:
                return true;
        }
    }

    public static void Matar()
    {
        pregunta1 = false;
        pregunta2 = false;
        pregunta3 = false;
        pregunta4 = false;
        pregunta5 = false;
        pregunta6 = false;
        pregunta7 = false;
        pregunta8 = false;
        pregunta9 = false;
        pregunta10 = false;

        buenas = 0;
        hechas = 0;
    }
}
