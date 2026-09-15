using System.Collections;
using UnityEngine;

public class PreguntasManager : MonoBehaviour
{
    public static PreguntasManager Instance { get; private set; }

    [Header("Preguntas")]
    [SerializeField] private GameObject Panel_Preguntas;
    public ContenedorPreguntas contenedorPreguntas = new ContenedorPreguntas();
    public DataPreguntas[] dataPreguntas;

    private int respuestaCorrecta;
    public int RespuestaCorrecta => respuestaCorrecta;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void MostrarPanelPreguntas()
    {
        StartCoroutine(EsperarAntesMostrarPreguntas());
    }

    public void AsignarPreguntasTXT()
    {
        int randomDataPregunta = Random.Range(0, dataPreguntas.Length); 
        
        contenedorPreguntas.preguntaTexto.text = dataPreguntas[randomDataPregunta].pregunta;

        contenedorPreguntas.opcion1.text = dataPreguntas[randomDataPregunta].opciones[0];
        contenedorPreguntas.opcion2.text = dataPreguntas[randomDataPregunta].opciones[1];
        contenedorPreguntas.opcion3.text = dataPreguntas[randomDataPregunta].opciones[2];
        contenedorPreguntas.opcion4.text = dataPreguntas[randomDataPregunta].opciones[3];

        respuestaCorrecta = dataPreguntas[randomDataPregunta].respuestaCorrecta;
    }

    public IEnumerator EsperarAntesMostrarPreguntas()
    {
        yield return new WaitForSeconds(1f);
        Panel_Preguntas.SetActive(true);
        AsignarPreguntasTXT();
    }
}
