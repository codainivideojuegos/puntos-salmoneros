using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BancoPreguntas", menuName = "Juego/Banco de Preguntas")]
public class BancoPreguntas : ScriptableObject
{
    [Tooltip("Lista original de preguntas. No se modifica en runtime.")]
    public List<Preguntas> preguntasOriginales = new List<Preguntas>();

    [System.NonSerialized]
    private List<Preguntas> preguntasRuntime;


    // ==========================================
    // OBTENCIÓN DE PREGUNTAS
    // ==========================================

    public Preguntas ObtenerPreguntaAleatoria()
    {
        if (preguntasRuntime == null)
            ReiniciarBanco();

        if (preguntasRuntime.Count == 0)
            return null;

        int indice = Random.Range(0, preguntasRuntime.Count);
        Preguntas pregunta = preguntasRuntime[indice];

        preguntasRuntime.RemoveAt(indice);

        return pregunta;
    }

    public int PreguntasRestantes()
    {
        if (preguntasRuntime == null)
            ReiniciarBanco();

        return preguntasRuntime.Count;
    }

    public int PreguntasEntregadas()
    {
        if (preguntasRuntime == null)
            ReiniciarBanco();

        return preguntasOriginales.Count - preguntasRuntime.Count;
    }


    // ==========================================
    // RESETEO (llamar manualmente al empezar partida nueva)
    // ==========================================

    public void ReiniciarBanco()
    {
        preguntasRuntime = new List<Preguntas>(preguntasOriginales);
    }
}