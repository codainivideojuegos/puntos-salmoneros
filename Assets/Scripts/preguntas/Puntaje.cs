using UnityEngine;

[CreateAssetMenu(fileName = "PuntajeData", menuName = "Juego/Puntaje")]
public class PuntajeData : ScriptableObject
{
    [System.NonSerialized]
    private int puntaje = 0;

    public int Puntaje => puntaje;


    // ==========================================
    // MODIFICACIÓN DE PUNTAJE
    // ==========================================

    public void Sumar(int cantidad = 1)
    {
        puntaje += cantidad;
    }


    // ==========================================
    // RESETEO (llamar manualmente al empezar partida nueva)
    // ==========================================

    public void Reiniciar()
    {
        puntaje = 0;
    }
}
