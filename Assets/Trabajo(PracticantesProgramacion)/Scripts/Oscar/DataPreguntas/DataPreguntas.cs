using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaPregunta", menuName = "Preguntas/Nueva Pregunta")]
public class DataPreguntas : ScriptableObject
{
    public string pregunta;

    public List<string> opciones = new List<string>();

    public int respuestaCorrecta; 
   
}
