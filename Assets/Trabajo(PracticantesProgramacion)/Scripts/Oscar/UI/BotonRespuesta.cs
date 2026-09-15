using UnityEngine;
using UnityEngine.UI;

public class BotonRespuesta : MonoBehaviour
{
    [SerializeField] private int numeroOpcion;

    private Button boton;

    private void Awake()
    {
        boton = GetComponent<Button>();
    }

    public void SeleccionarRespuesta()
    {
        ButtonManager.Instance.EsRespuestaCorrecta(numeroOpcion, boton);
    }
}
