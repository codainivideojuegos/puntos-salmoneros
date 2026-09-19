using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelOpciones;
    public BancoPreguntas preguntas;
    public PuntajeData puntaje;

    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1");
        preguntas.ReiniciarBanco();
        puntaje.Reiniciar();
    }

    public void Opciones()
    {
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
    }
}