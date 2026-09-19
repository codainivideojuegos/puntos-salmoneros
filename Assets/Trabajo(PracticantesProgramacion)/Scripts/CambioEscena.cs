using UnityEngine;

public class CambioEscena : MonoBehaviour
{
    public Transform puntoInicial;
    public GameObject Jugador;
    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Player");
        puntoInicial = GameObject.FindGameObjectWithTag("PuntoInicial").transform;
        MoverAPuntoInicial();
    }
    public void MoverAPuntoInicial()
    {
        Jugador.transform.position = puntoInicial.position;
    }
}
