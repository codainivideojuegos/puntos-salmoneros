using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test2 : MonoBehaviour
{
    [Header("Preguntas")]
    public BancoPreguntas bancoPreguntas;

    [Header("UI General")]
    public GameObject canva;
    public GameObject botonConfirmar;

    [Header("Sprites de botones")]
    public Sprite spriteNormal;
    public Sprite spriteMarcado;

    [Header("Referencias")]
    public Conter counter;

    [Header("Obstaculos")]
    [SerializeField] private GeneradorNivel Generador;

    [Header("Textos")]
    public TMP_Text textopregunta;
    public TMP_Text textorespuesta1;
    public TMP_Text textorespuesta2;
    public TMP_Text textorespuesta3;
    public TMP_Text textorespuesta4;

    [Header("Botones")]
    public Button[] botonesRespuestas;

    [Header("Feedback")]
    [SerializeField] private float tiempoFeedback = 1f;

    [Header("Configuración del nivel")]
    [Tooltip("Cuántas preguntas en TOTAL (contando todos los niveles anteriores) deben haberse entregado para pasar de este nivel. Ej: Nivel 1 = 4, Nivel 2 = 7 (4+3), Nivel 3 = 10 (4+3+3).")]
    [SerializeField] private int umbralPreguntasParaAvanzar = 4;
    [Tooltip("Marcar solo en el último nivel del juego.")]
    [SerializeField] private bool esUltimoNivel = false;
    [Tooltip("Nombre de la escena siguiente. No se usa si 'Es Ultimo Nivel' está activo.")]
    [SerializeField] private string escenaSiguiente;

    private Preguntas preguntaActual;
    private bool[] respuestasMarcadas = new bool[4];
    private bool esperandoFeedback = false;


    // ==========================================
    // CICLO DE VIDA
    // ==========================================

    void Start()
    {
        canva.SetActive(false);
        Generador = GameObject.FindGameObjectWithTag("GeneradorObstaculos").GetComponent<GeneradorNivel>();
        Debug.Log(bancoPreguntas.PreguntasRestantes());
    }


    // ==========================================
    // FLUJO DE PREGUNTAS
    // ==========================================

    public void NuevaPregunta()
    {
        esperandoFeedback = false;

        preguntaActual = bancoPreguntas.ObtenerPreguntaAleatoria();

        if (preguntaActual == null)
        {
            // No quedan preguntas en el banco: decidir a dónde ir en vez de seguir con obstáculos a ciegas.
            canva.SetActive(false);
            ManejarSinPreguntasDisponibles();
            return;
        }

        canva.SetActive(true);

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

        botonConfirmar.SetActive(preguntaActual.boolquedefinesiesESApregunta);
    }

    private void EmpezarObstaculos()
    {
        Generador.Empezar();
    }

    private void ManejarFinDePregunta()
    {
        int entregadas = bancoPreguntas.PreguntasEntregadas();
        bool quedanPreguntasEnBanco = bancoPreguntas.PreguntasRestantes() > 0;
        bool alcanzoUmbralDeNivel = entregadas >= umbralPreguntasParaAvanzar;

        if (!alcanzoUmbralDeNivel && quedanPreguntasEnBanco)
        {
            // Todavía faltan preguntas para completar este nivel: seguimos con obstáculos.
            EmpezarObstaculos();
        }
        else
        {
            ManejarSinPreguntasDisponibles();
        }
    }

    private void ManejarSinPreguntasDisponibles()
    {
        if (esUltimoNivel || bancoPreguntas.PreguntasRestantes() == 0)
        {
            // Es el último nivel, o no queda ninguna pregunta más en el banco: mostramos el resultado final.
            counter.MostrarResultadoFinal(bancoPreguntas.preguntasOriginales.Count);
        }
        else
        {
            // Se cumplió el umbral de este nivel y todavía hay preguntas para los niveles siguientes.
            SceneManager.LoadScene(escenaSiguiente);
        }
    }


    // ==========================================
    // RESPUESTA SIMPLE (una sola opción correcta)
    // ==========================================

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

            botonesRespuestas[indice].image.sprite =
                respuestasMarcadas[indice] ? spriteMarcado : spriteNormal;

            return;
        }

        int indiceRespuesta = respuesta - 1;

        if (indiceRespuesta < 0 || indiceRespuesta >= botonesRespuestas.Length)
            return;

        bool correcto = respuesta == preguntaActual.repuetanume;

        if (correcto)
        {
            counter.SumarPunto();
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

        botonesRespuestas[indiceBoton].image.color = correcto ? Color.green : Color.red;

        yield return new WaitForSeconds(tiempoFeedback);

        botonesRespuestas[indiceBoton].image.color = Color.white;

        canva.SetActive(false);

        preguntaActual = null;
        respuestasMarcadas = new bool[4];

        esperandoFeedback = false;

        ManejarFinDePregunta();
    }


    // ==========================================
    // RESPUESTA MÚLTIPLE (varias opciones correctas)
    // ==========================================

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
            counter.SumarPunto();
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
                botonesRespuestas[i].image.color = respuestaCorrecta ? Color.green : Color.red;
            }
            // gracias oscar
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

        ManejarFinDePregunta();
    }


    // ==========================================
    // UTILIDADES
    // ==========================================

    private bool ObtenerRespuestaCorrecta(int indice)
    {
        switch (indice)
        {
            case 0: return preguntaActual.respuesta1Correcta;
            case 1: return preguntaActual.respuesta2Correcta;
            case 2: return preguntaActual.respuesta3Correcta;
            case 3: return preguntaActual.respuesta4Correcta;
            default: return false;
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